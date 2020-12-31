using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.Models
{
    public class BusTicket
    {
        public int BusTicketId { get; set; }
        public string TicketNumber { get; set; }
        public int SeatNumber { get; set; }
        [Required]
        public Destination Destination { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime();
        public string CreatedBy { get; set; }
    }
}
