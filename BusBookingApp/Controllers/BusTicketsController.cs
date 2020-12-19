using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBookingApp.Data.Models;
using BusBookingApp.Helpers;
using BusBookingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusTicketsController : ControllerBase
    {
        private readonly IBusTicketRepository _busTicketRepository;

        public BusTicketsController(IBusTicketRepository busTicketRepository)
        {
            _busTicketRepository = busTicketRepository;
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
                        return Created("api/busTickets/create", WebHelpers.GetReturnObject(busTicket, true, "Ticket created successfully"));
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
                //remember to make it optional to get all tickets by user or not
                var data = await _busTicketRepository.GetAllByUserAsync();
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
