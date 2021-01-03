using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.DTOs
{
    public class UserDTO
    {
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
    }
}
