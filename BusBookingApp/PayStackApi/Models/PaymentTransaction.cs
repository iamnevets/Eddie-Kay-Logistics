using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.PayStackApi.Models
{
    public class PaymentTransaction
    {
        public long PaymentTransactionId { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string UserId { get; set; }
        public string MobileMoneyAccPhoneNumber { get; set; }
        [NotMapped]
        public Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        public string Cancel_action { get; set; }
    }
}
