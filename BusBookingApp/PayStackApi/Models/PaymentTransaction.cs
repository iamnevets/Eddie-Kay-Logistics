using BusBookingApp.Data;
using BusBookingApp.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusBookingApp.PayStackApi.Models
{
    public class PaymentTransaction
    {
        public long PaymentTransactionId { get; set; }
        [Required]
        public string TransactionReference { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string UserId { get; set; }
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int StudentId { get; set; }
        [Required]
        public string TransactionStatus { get; set; }
    }
}
