using BusBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public interface IBusRepository : IGeneralRepository
    {
        Task<Bus> GetBusAsync(int busId);
        Task<List<Bus>> GetAllBusesAsync();
        Task<bool> UpdateBusAsync(Bus busToUpdate, Bus bus);
    }
}
