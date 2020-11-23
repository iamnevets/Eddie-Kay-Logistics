using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data.Models
{
    public class Privileges
    {
        //Permissions on the User Model
        public const string CanCreateUsers = "CanCreateUsers";
        public const string CanViewUsers = "CanViewUsers";
        public const string CanUpdateUsers = "CanUpdateUsers";
        public const string CanDeleteUsers = "CanDeleteUsers";

        //Permissions on Bus
        public const string CanCreateBus = "CanCreateBus";
        public const string CanViewBus = "CanViewBus";
        public const string CanUpdateBus = "CanUpdateBus";
        public const string CanDeleteBus = "CanDeleteBus";

        //Permissions on Ticket
        public const string CanCreateTicket = "CanCreateTicket";
        public const string CanViewTicket = "CanDeleteTicket";

        //Permissions on the Role
        //public const string CanCreateRoles = "CanCreateRoles";
        //public const string CanViewRoles = "CanViewRoles";
        //public const string CanUpdateRoles = "CanUpdateRoles";
        //public const string CanDeleteRoles = "CanDeleteRoles";

        //permissions on Settings
        //public const string CanCreateSettings = "CanCreateSettings";
        //public const string CanViewSettings = "CanViewSettings";
        //public const string CanUpdateSettings = "CanUpdateSettings";
        //public const string CanDeleteSettings = "CanDeleteSettings";
    }
}
