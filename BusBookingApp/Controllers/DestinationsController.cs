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
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepository;

        public DestinationsController(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Destination destination)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _destinationRepository.Add(destination);

                    if (await _destinationRepository.SaveChangesAsync())
                        return Created("api/buses/create", WebHelpers.GetReturnObject(destination, true, "Destination created successfully"));
                }

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Could not create destination"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpGet("{destinationId:int}")]
        public async Task<ActionResult> Get(int destinationId)
        {
            try
            {
                //var destination = await _destinationRepository.GetBusAsync(destinationId);
                var destination = await _destinationRepository.GetAsync<Destination>(destinationId);
                if (destination == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Could not find the destination"));

                return Ok(WebHelpers.GetReturnObject(destination, true, "Successful"));

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
                //var data = await _destinationRepository.GetAllBusesAsync();
                var data = await _destinationRepository.GetAllAsync<Destination>();
                return Ok(WebHelpers.GetReturnObject(data, true, "Successful"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpPut("{destinationId:int}")]
        public async Task<ActionResult> Update(int destinationId, Destination destination)
        {
            try
            {
                var busToUpdate = await _destinationRepository.GetAsync<Destination>(destinationId);
                if (busToUpdate == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Destination could not be found. Please update an existing destination!"));

                if (await _destinationRepository.UpdateAsync(destination))
                    return Created("api/buses/update", WebHelpers.GetReturnObject(destination, true, "Destination has been updated successfully"));

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Could not update destination"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpDelete("{destinationId:int}")]
        public async Task<ActionResult> Delete(int destinationId)
        {
            try
            {
                var busToDelete = await _destinationRepository.GetAsync<Destination>(destinationId);
                if (busToDelete == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Destination could not be found. Please delete an existing destination!"));

                _destinationRepository.Delete(busToDelete);
                if (await _destinationRepository.SaveChangesAsync())
                    return Ok(WebHelpers.GetReturnObject(null, true, "Destination deleted successfully"));

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Failed to delete destination"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }

        }
    }
}
