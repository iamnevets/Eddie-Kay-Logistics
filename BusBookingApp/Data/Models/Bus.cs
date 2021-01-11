using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.Models
{
    public class BusType
    {
        public const string Economy = "Economy";
        public const string Executive = "Executive";
    }

    public class Bus
    {
        public int BusId { get; set; }
        [Required, MaxLength(10)]
        public string BusNumber { get; set; }
        [Required]
        public string BusType { get; set; }
        public int? DestinationId { get; set; }
        public Destination Destination { get; set; }
        public string Price { get; set; }
        public virtual List<BusTicket> BusTicket { get; set; }
    }
}
