using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.Models
{
    public class AccountModels
    {
        public class LoginModel
        {
            [Required(ErrorMessage = "User Name is required")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password is required")]
            public string Password { get; set; }
            public bool RememberMe { get; set; } = true;
        }

        public class ChangePasswordModel
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }

            [Compare("NewPassword", ErrorMessage = "Password doesn't match")]
            public string ConfirmPassword { get; set; }
        }

        public class ResetPasswordModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }

            [Compare("Password", ErrorMessage = "Password doesn't match")]
            public string ConfirmPassword { get; set; }
        }
    }
}
