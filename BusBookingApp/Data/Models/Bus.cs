using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.Models
{
    public enum BusType
    {
        Economy,
        Executive
    }

    public class Bus
    {
        public int BusId { get; set; }
        [Required, MaxLength(10)]
        public string BusNumber { get; set; }
        public BusType BusType { get; set; }
        [Required]
        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }
        public string Price { get; set; }
    }
}
