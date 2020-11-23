using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.Models
{
    public class Bus
    {
        public int BusId { get; set; }
        [Required, MaxLength(10)]
        public string BusNumber { get; set; }
        public int NumberOfSeats { get; set; } = 60;
    }
}
