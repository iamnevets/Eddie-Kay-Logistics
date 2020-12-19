using BusBookingApp.Data;
using BusBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public interface IBusTicketRepository : IGeneralRepository
    {
        Task<List<BusTicket>> GetAllByUserAsync();
        string CreateTicketNumber();
        string PayForTicket();
        User GetCurrentUser();
    }
}
