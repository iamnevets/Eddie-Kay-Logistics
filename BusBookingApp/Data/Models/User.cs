using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusBookingApp.Data
{
    public class User : IdentityUser
    {
        [MaxLength(128), Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public bool IsDeleted { get; set; } = false;
        public bool Locked { get; set; } = true;
        public bool Hidden { get; set; } = false;
        //public virtual ICollection<UserClaim> Claims { get; set; }
        //public virtual ICollection<UserLogin> Logins { get; set; }
        //public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserToken> Tokens { get; set; }
        
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }

    public class UserToken : IdentityUserToken<string>
    {
        public virtual User User { get; set; }
    }
}
