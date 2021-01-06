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
    }
}
