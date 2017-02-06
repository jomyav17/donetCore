using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Trips", "App");
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "User name or Password incorrect");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }
    }
}
