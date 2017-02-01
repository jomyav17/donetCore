using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private readonly ILogger<TripsController> _logger;
        private readonly IWorldRepository _repository;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTrips()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while retrieving trips: {ex}");

                return BadRequest("Error occured");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel thetrip)
        {
            if (ModelState.IsValid)
            {
                _repository.AddTrip(Mapper.Map<Trip>(thetrip));
                if (await _repository.SaveChanges())
                    return Created($"api/trips/{thetrip.Name}", thetrip /*Mapper.Map<TripViewModel>(Mapper.Map<Trip>(thetrip))*/);
            }

            return BadRequest("Couldn't save trip");
        }
    }
}
