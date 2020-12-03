using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusBookingApp.Data;
using BusBookingApp.Helpers;
using BusBookingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using static BusBookingApp.Data.Models.AccountModels;

namespace BusBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        private readonly UserRepository _userRepository;

        public AccountsController(IServiceProvider serviceProvider, IConfiguration configuration/*, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, ApplicationDbContext dbContext*/)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            //_userManager = userManager;
            //_roleManager = roleManager;
            //_signInManager = signInManager;
            //_dbContext = dbContext;
            _userManager = serviceProvider.GetService<UserManager<User>>();
            _roleManager = serviceProvider.GetService<RoleManager<Role>>();
            _dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            _signInManager = serviceProvider.GetService<SignInManager<User>>();
            _userRepository = new UserRepository(_dbContext);
        }

        #region Login
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginModel.Username);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe);
                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        var claims = new List<Claim>
                        {
                            new Claim("Id", user.Id),
                            new Claim("UserName", user.UserName),
                            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            //new Claim(ClaimTypes.Role, userRoles.FirstOrDefault())
                        };

                        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: claims,
                        signingCredentials: signingCredentials
                        );
                        var tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

                        var data = new
                        {
                            user.Id,
                            Username = user.UserName,
                            user.Name,
                            user.Email,
                            user.PhoneNumber,
                            user.Role,
                            Token = tokenResponse
                        };

                        return Ok(new
                        {
                            data,
                            Message = "Login successful",
                            Success = true
                        });
                    }

                    return BadRequest("Invalid username or password");
                }

                return BadRequest("Invalid username or password");
            }
            catch (Exception e)
            {
                return BadRequest(WebHelpers.ProcessException(e));
            }
        }
        #endregion Login


        #region Create user or SignUp
        [HttpPost]
        [Route("signup")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser(User userModel)
        {
            try
            {
                userModel.Id = Guid.NewGuid().ToString();

                var result = await _userManager.CreateAsync(userModel, userModel.Password).ConfigureAwait(true);

                if (result.Succeeded)
                {
                    var user = _userManager.FindByNameAsync(userModel.UserName).Result;
                    //Add to role
                    var rslt = _userManager.AddToRoleAsync(user, userModel.Role);
                }
                else
                    return BadRequest(WebHelpers.ProcessException(result));

                return Created("CreateUser", new { userModel.Id, Message = "User has been created Successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(WebHelpers.ProcessException(e));
            }
        }
        #endregion


        #region UpdateUser

        [HttpPut]
        [Route("updateuser")]
        public async Task<ActionResult> UpdateUser(User UserModel)
        {
            try
            {
                var user = _userRepository.Get(UserModel.Id);

                if (user == null) return NotFound("User not found. Please update an existing user");

                if(await _userRepository.Update(user))
                    return Created("UpdateUser", new { user.Id, Message = "User has been updated successfully" });

                return BadRequest("Could not update user");
            }
            catch (Exception e)
            {
                return BadRequest(WebHelpers.ProcessException(e));
            }
        }

        #endregion UpdateUser


        #region DeleteUser
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var userToDelete = _userRepository.Get(id);
                if (userToDelete == null) return NotFound("User not found!");

                _userRepository.Delete(userToDelete);
                if(await _userRepository.SaveChangesAsync())
                    return Ok(new { Message = "User Deleted Successfully." });

                return BadRequest("Failed to delete user");
            }
            catch (Exception ex)
            {
                return BadRequest(WebHelpers.ProcessException(ex));
            }
        }
        #endregion DeleteUser


        #region GetUsers

        [HttpGet]
        [Route("GetUsers")]
        public ActionResult GetUsers()
        {
            try
            {
                var result = _userRepository.GetAll();
                var data = result.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Email,
                    x.PhoneNumber,
                    x.UserName,
                    x.StudentId
                });

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(WebHelpers.ProcessException(e));
            }
        }

        #endregion GetUsers


        #region Logout
        [HttpGet]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok(new { Message = "User Logged Out" });
            }
            catch (Exception ex)
            {
                return BadRequest(WebHelpers.ProcessException(ex));
            }
        }
        #endregion Logout
    }
}
