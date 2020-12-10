using System;
using System.Threading.Tasks;
using BusBookingApp.Data;
using BusBookingApp.Data.Models;
using BusBookingApp.Helpers;
using BusBookingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusesController : ControllerBase
    {
        private readonly DbContext _dbContext;
        private readonly IBusRepository _busRepository;

        public BusesController(IServiceProvider serviceProvider, IBusRepository busRepository)
        {
            _dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            _busRepository = busRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Bus bus)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _busRepository.Add(bus);

                    if (await _busRepository.SaveChangesAsync())
                        return Created("api/buses/create", WebHelpers.GetReturnObject(bus, true, "Bus created successfully"));
                }

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Could not create bus"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failed");
            }
        }

        [HttpGet("{busId:int}")]
        public async Task<ActionResult> Get(int busId)
        {
            try
            {
                var bus = await _busRepository.GetBusAsync(busId);
                if (bus == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Could not find the bus"));

                return Ok(WebHelpers.GetReturnObject(bus, true, "Successful"));

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
                var data = await _busRepository.GetAllBusesAsync();
                return Ok(WebHelpers.GetReturnObject(data, true, "Successful"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpPut("{busId:int}")]
        public async Task<ActionResult> Update(int busId, Bus bus)
        {
            try
            {
                var busToUpdate = await _busRepository.GetBusAsync(busId);
                if (busToUpdate == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Bus could not be found. Please update an existing bus!"));

                if (await _busRepository.UpdateBusAsync(busToUpdate, bus))
                    return Created("api/buses/update", WebHelpers.GetReturnObject(bus, true, "Bus has been updated successfully"));

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Could not update bus"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpDelete("{busId:int}")]
        public async Task<ActionResult> Delete(int busId)
        {
            try
            {
                var busToDelete = await _busRepository.GetBusAsync(busId);
                if (busToDelete == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Bus could not be found. Please delete an existing bus!"));

                _busRepository.Delete(busToDelete);
                if(await _busRepository.SaveChangesAsync())
                    return Ok(WebHelpers.GetReturnObject(null, true, "Bus deleted successfully"));

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Failed to delete bus"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }
    }
}
