using BusBookingApp.Data;
using BusBookingApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public interface IBusTicketRepository : IGeneralRepository
    {
        Task<List<BusTicket>> GetAllByUserAsync();
        string CreateTicketNumber();
        User GetCurrentUser();
    }
}
