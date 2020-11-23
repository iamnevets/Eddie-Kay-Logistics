using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.Models
{
    public class BusTicket
    {
        public int TicketId { get; set; }
        [Required]
        public int SeatNumber { get; set; }
        [Required]
        public string Destination { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
