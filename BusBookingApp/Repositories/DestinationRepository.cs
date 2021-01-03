using BusBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public class DestinationRepository : GeneralRepository, IDestinationRepository
    {
        public DestinationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
