﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.Models
{
    public enum Status
    {
        Pending,
        Successful,
        Failed
    }
    public class BusTicket
    {
        public int BusTicketId { get; set; }
        public string TicketNumber { get; set; }
        public int? BusId { get; set; }
        public Bus Bus { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime().Date;
        public string CreatedBy { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public string NewStatus { get; set; } = "Pending";
    }
}
