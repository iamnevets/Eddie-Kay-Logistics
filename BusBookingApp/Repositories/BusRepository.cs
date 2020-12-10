using BusBookingApp.Data;
using BusBookingApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public class BusRepository : GeneralRepository, IBusRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Bus> GetBusAsync(int busId)
        {
          return await _dbContext.Buses.FindAsync(busId);
        }

        public async Task<List<Bus>> GetAllBusesAsync()
        {
            return await _dbContext.Buses.AnyAsync() ? await _dbContext.Buses.ToListAsync() : throw new Exception("There are no buses");
        }

        public async Task<bool> UpdateBusAsync(Bus busToUpdate, Bus bus)
        {
            bus.BusNumber = busToUpdate.BusNumber;
            bus.NumberOfSeats = busToUpdate.NumberOfSeats;

            return await SaveChangesAsync();
        }
    }
}
