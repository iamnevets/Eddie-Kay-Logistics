using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBookingApp.Data;
using BusBookingApp.Data.Models;
using BusBookingApp.Helpers;
using BusBookingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusTicketsController : ControllerBase
    {
        private readonly IBusTicketRepository _busTicketRepository;
        private readonly ApplicationDbContext _dbContext;

        public BusTicketsController(IBusTicketRepository busTicketRepository, ApplicationDbContext dbContext)
        {
            _busTicketRepository = busTicketRepository;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Create(BusTicket busTicket)
        {
            try
            {
                busTicket.TicketNumber = _busTicketRepository.CreateTicketNumber();
                busTicket.CreatedBy = _busTicketRepository.GetCurrentUser().UserName;
                if (ModelState.IsValid)
                {
                    _busTicketRepository.Add(busTicket);

                    if (await _busTicketRepository.SaveChangesAsync())
                    {
                        var returnData = _dbContext.BusTickets.Where(x => x.BusTicketId == busTicket.BusTicketId).Include(x => x.Bus).Select(x => new
                        {
                            x.BusTicketId,
                            x.TicketNumber,
                            x.SeatNumber,
                            x.Bus,
                            x.Date,
                            x.CreatedBy
                        });

                        return Created("api/busTickets/create", WebHelpers.GetReturnObject(returnData, true, "Ticket created successfully"));
                    }
                }

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Could not create Ticket"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpGet("{busTicketId:int}")]
        public async Task<ActionResult> Get(int busTicketId)
        {
            try
            {
                var ticket =await _busTicketRepository.GetAsync<BusTicket>(busTicketId);
                if(ticket == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Could not find the ticket"));

                return Ok(WebHelpers.GetReturnObject(ticket, true, "Successful"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var currentUser = _busTicketRepository.GetCurrentUser();
                var currentUserRole = _dbContext.Roles.Where(x => x.Id == currentUser.UserRoles.FirstOrDefault().RoleId).Include(x => x.RoleClaims).FirstOrDefault().Name;

                // Get all bus tickets in the system if user is Admin, else only get the tickets for a particular user
                List<BusTicket> data;
                if (currentUserRole == "Administrator")
                {
                    data = await _busTicketRepository.GetAllAsync<BusTicket>();
                    data = data.OrderBy(x => x.CreatedBy).ToList();
                }
                else
                {
                    data = await _busTicketRepository.GetAllByUserAsync();
                }
                return Ok(WebHelpers.GetReturnObject(data, true, "Successful"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpPut("{busTicketId:int}")]
        public async Task<ActionResult> Update(int busTicketId, BusTicket busTicket)
        {
            try
            {
                var ticketToUpdate = await _busTicketRepository.GetAsync<Bus>(busTicketId);
                if (ticketToUpdate == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Ticket could not be found. Please update an existing ticket!"));

                if (await _busTicketRepository.UpdateAsync(busTicket))
                    return Created("api/tickets/update", WebHelpers.GetReturnObject(busTicket, true, "Ticket has been updated successfully"));

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Could not update Ticket"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpDelete("{busTicketId:int}")]
        public async Task<ActionResult> Delete(int busTicketId)
        {
            try
            {
                var ticketToDelete = await _busTicketRepository.GetAsync<Bus>(busTicketId);
                if (ticketToDelete == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Ticket could not be found. Please delete an existing ticket!"));

                _busTicketRepository.Delete(ticketToDelete);
                if (await _busTicketRepository.SaveChangesAsync())
                    return Ok(WebHelpers.GetReturnObject(null, true, "Ticket deleted successfully"));

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Failed to delete Ticket"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }
    }
}
