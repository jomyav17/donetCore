using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/Stops")]
    public class StopsController : Controller
    {
        private readonly GeoCodeService _geoService;
        private readonly ILogger<StopsController> _logger;
        private readonly IWorldRepository _repository;

        public StopsController(IWorldRepository repository,
            ILogger<StopsController> logger,
            GeoCodeService geoService)
        {
            _repository = repository;
            _logger = logger;
            _geoService = geoService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(
                    _repository.GetTripByName(tripName).Stops.OrderBy(s => s.Order)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while retrieving stops for trip name:{tripName}\nError:{ex}");
            }

            return BadRequest("Failed to get stops");
        }

        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel stop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var geoResult = await _geoService.GetCordsAsync(stop.Name);
                    if (geoResult.Success)
                    {
                        stop.Latitude = geoResult.Latitude;
                        stop.Longitude = geoResult.Longitude;
                        _repository.AddStop(tripName, Mapper.Map<Stop>(stop));
                        if (await _repository.SaveChanges())
                            return Created($"api/trips/{tripName}/Stops/{stop.Name}", stop);
                    }
                    else
                    {
                        _logger.LogError(geoResult.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while saving stop : {ex}");
            }
            return BadRequest("Error occured while saving stop");
        }
    }
}
