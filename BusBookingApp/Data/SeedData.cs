using BusBookingApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusBookingApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var roleManager = serviceProvider.GetService<RoleManager<Role>>();
            
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                #region Roles

                //Admin Role
                var adminPrivileges = new List<string>
                        {
                            Privileges.CanViewUsers,
                            Privileges.CanCreateUsers,
                            Privileges.CanUpdateUsers,
                            Privileges.CanDeleteUsers,
                            Privileges.CanCreateBus,
                            Privileges.CanViewBus,
                            Privileges.CanUpdateBus,
                            Privileges.CanDeleteBus
                };
                var adminRole = new Role { Name = "Administrator", NormalizedName = "Administrator" };
                var existingAdminRole = context.Roles.FirstOrDefault(x => x.Name == adminRole.Name);
                if (existingAdminRole == null)
                {
                    var res = roleManager.CreateAsync(adminRole);
                    if (res.Result.Succeeded)
                    {
                        adminPrivileges.Distinct().ToList().ForEach(r => roleManager.AddClaimAsync(adminRole,
                        new Claim("Privilege", r)).Wait());
                    }
                }
                else
                {
                    foreach (var privilege in adminPrivileges)
                    {
                        var exst = context.RoleClaims.FirstOrDefault(x => x.RoleId == existingAdminRole.Id && privilege == x.ClaimValue);
                        if (exst == null)
                        {
                            var newClaim = new RoleClaim { RoleId = existingAdminRole.Id, ClaimValue = privilege, ClaimType = "Privilege" };
                            context.RoleClaims.Add(newClaim);
                            context.SaveChanges();
                            var a = newClaim;
                        }
                    }
                }

                //End User Role
                var endUserPrivileges = new List<string>
                {
                    Privileges.CanCreateTicket,
                    Privileges.CanViewTicket
                };

                var endUserRole = new Role { Name = "EndUser", NormalizedName = "EndUser" };
                var existingEndUserRole = context.Roles.FirstOrDefault(x => x.Name == endUserRole.Name);
                if (existingEndUserRole == null)
                {
                    var res = roleManager.CreateAsync(endUserRole);
                    if (res.Result.Succeeded)
                    {
                        endUserPrivileges.Distinct().ToList().ForEach(r => roleManager.AddClaimAsync(endUserRole,
                        new Claim("Privilege", r)).Wait());
                    }
                }
                else
                {
                    foreach (var privilege in endUserPrivileges)
                    {
                        var exists = context.RoleClaims.FirstOrDefault(x => x.RoleId == existingEndUserRole.Id && privilege == x.ClaimValue);
                        if (exists == null)
                        {
                            var newClaim = new RoleClaim { RoleId = existingEndUserRole.Id, ClaimValue = privilege, ClaimType = "Privilege" };
                            context.RoleClaims.Add(newClaim);
                            context.SaveChanges();
                            var a = newClaim;
                        }
                    }
                }

                #endregion

                #region Users
                var adminUser = new User
                {
                    Name = "System Administrator",
                    UserName = "Admin"
                };
                var existingUser = userManager.FindByNameAsync("Admin").Result;
                //Admin User
                if (existingUser == null)
                {
                    var res = userManager.CreateAsync(adminUser, "admin@password");
                    if (res.Result.Succeeded)
                    {
                        var user = userManager.FindByNameAsync("Admin").Result;
                        userManager.AddToRoleAsync(user, adminRole.Name).Wait();
                    }
                }
                #endregion

                context.SaveChanges();
            }
        }
    }
}
