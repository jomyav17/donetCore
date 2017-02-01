using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IConfigurationRoot _config;
        private readonly IMailService _mailService;
        private readonly IWorldRepository _repository;
        private readonly ILogger<AppController> _logger;

        public AppController(IMailService service, IConfigurationRoot config, IWorldRepository repository, ILogger<AppController> logger)
        {
            _mailService = service;
            _config = config;
            _repository = repository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Trips()
        {
            var data = _repository.GetAllTrips();
            return View(data);
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            try
            {

                if (model.Email.Contains("aol.com"))
                    ModelState.AddModelError("Email", "We don't support aol email");

                if (ModelState.IsValid)
                    _mailService.SendMail(_config["MailSetting:ToAddress"], model.Email, $"Message from {model.Name}", model.Message);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while sending the message. {ex.Message}");
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
