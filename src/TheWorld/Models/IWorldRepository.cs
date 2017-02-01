﻿using System.Collections.Generic;
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
    }
}