using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext _context;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, Stop stop)
        {
            var trip = GetTripByName(tripName);
            if (trip != null)
            {
                trip.Stops.Add(stop);
                _context.Stops.Add(stop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Trips.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Fetching all trips");
            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips.Where(t => t.Name == tripName)
                .Include(i => i.Stops)
                .FirstOrDefault();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
