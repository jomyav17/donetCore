using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop stop);

        Task<bool> SaveChanges();

        Trip GetTripByName(string tripName);
        IEnumerable<Trip> GetTripsByUsername(string userName);
        Trip GetTripByName(string tripName, string userName);
        void AddStop(string tripName, Stop stop, string userName);
    }
}