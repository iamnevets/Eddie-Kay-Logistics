using BusBookingApp.Data;
using BusBookingApp.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public class BusTicketRepository : GeneralRepository, IBusTicketRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly User _currentUser;

        public BusTicketRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = GetCurrentUser();
        }

        public async Task<List<BusTicket>> GetAllByUserAsync()
        {
            var tickets = (IQueryable<BusTicket>)_dbContext.BusTickets
                .Where(x => x.CreatedBy == _currentUser.UserName)
                .Include(x => x.Bus)
                .ThenInclude(x => x.Destination)
                .Select(x => new
                {
                    x.BusTicketId,
                    x.TicketNumber,
                    Bus = new
                    {
                        x.Bus.BusId,
                        x.Bus.BusNumber,
                        x.Bus.BusType,
                        Destination = x.Bus.Destination.Name,
                        x.Bus.Price,
                        x.Bus.PickupPoint,
                        x.Bus.PickupDate
                    },
                    x.Date,
                    x.CreatedBy,
                    x.Status
                });

            return await tickets.ToListAsync();
        }

        public string CreateTicketNumber()
        {
            var time = DateTime.UtcNow.ToString("hh:mm:ss").Replace(":", "");
            var date = DateTime.UtcNow.Date.ToShortDateString().Replace("/", "");
            Console.WriteLine(date);

            var lastThreeDigitsOfPhoneNumber = _currentUser.PhoneNumber.Substring(6, 3);

            var ticketNumber = $"GH-{date}-{time}-{lastThreeDigitsOfPhoneNumber}";
            return ticketNumber;
        }

        public User GetCurrentUser()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;
            var currentUser = _dbContext.Users.Where(x => x.Id == currentUserId).Include(x => x.UserRoles).Include(x => x.Claims).FirstOrDefault();

            return currentUser;
        }
    }
}
