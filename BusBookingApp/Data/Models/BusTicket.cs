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
        public int? BusId { get; set; }
        public Bus Bus { get; set; }
        public string PickupPoint { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime().Date;
        public string CreatedBy { get; set; }
    }
}
