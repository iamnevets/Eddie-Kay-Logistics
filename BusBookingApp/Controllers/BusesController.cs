using System;
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
using Microsoft.Extensions.DependencyInjection;

namespace BusBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusesController : ControllerBase
    {
        private readonly IBusRepository _busRepository;
        private readonly ApplicationDbContext _dbContext;

        public BusesController(IServiceProvider serviceProvider, IBusRepository busRepository, ApplicationDbContext dbContext)
        {
            _busRepository = busRepository;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Bus bus)
        {
            try
            {
                if(ModelState.IsValid)
                {
                   if(bus.BusType == BusType.Economy || bus.BusType == BusType.Executive)
                    {
                        _busRepository.Add(bus);

                        if (await _busRepository.SaveChangesAsync())
                        {
                            var returnData = _dbContext.Buses.Where(x => x.BusId == bus.BusId).Include(x => x.Destination).Select(x => new
                            {
                                x.BusId,
                                x.BusNumber,
                                x.BusType,
                                Destination = x.Destination.Name,
                                x.PickupPoint,
                                x.PickupDate,
                                x.Price
                            });
                            return Created("api/buses/create", WebHelpers.GetReturnObject(returnData, true, "Bus created successfully"));
                        }
                    }
                   else
                        return BadRequest(WebHelpers.GetReturnObject(null, false, $"Could not create bus. BusType should either be {BusType.Economy} or {BusType.Executive}"));
                }

                return BadRequest(WebHelpers.GetReturnObject(null, false, "Could not create bus"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpGet("{busId:int}")]
        public async Task<ActionResult> Get(int busId)
        {
            try
            {
                //var bus = await _busRepository.GetBusAsync(busId);
                var bus = _dbContext.Buses.Where(x => x.BusId == busId).Include(x => x.Destination).Select(x => new
                {
                    x.BusId,
                    x.BusNumber,
                    x.BusType,
                    Destination = x.Destination.Name,
                    x.PickupPoint,
                    x.PickupDate,
                    x.Price
                });

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
                //var data = await _busRepository.GetAllBusesAsync();
                var data = _dbContext.Buses.Include(x => x.Destination).Select(x => new
                {
                    x.BusId,
                    x.BusNumber,
                    x.BusType,
                    Destination = x.Destination.Name,
                    x.PickupPoint,
                    x.PickupDate,
                    x.Price
                }).ToList();
                if(data.Any())
                {
                    
                    return Ok(WebHelpers.GetReturnObject(data, true, "Successful"));
                }
                else
                    return Ok(WebHelpers.GetReturnObject(data, true, "There are no buses"));
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
                if(bus.BusType == BusType.Economy || bus.BusType == BusType.Executive)
                {
                    var busToUpdate = await _busRepository.GetAsync<Bus>(busId);
                    if (busToUpdate == null)
                        return NotFound(WebHelpers.GetReturnObject(null, false, "Bus could not be found. Please update an existing bus!"));

                    if (await _busRepository.UpdateAsync(bus))
                    {
                        var updatedBus = _dbContext.Buses.Where(x => x.BusId == busId).Include(x => x.Destination).Select(x => new
                        {
                            x.BusId,
                            x.BusNumber,
                            x.BusType,
                            Destination = x.Destination.Name,
                            x.PickupPoint,
                            x.PickupDate,
                            x.Price
                        });

                        return Created("api/buses/update", WebHelpers.GetReturnObject(updatedBus, true, "Bus has been updated successfully"));
                    }

                    return BadRequest(WebHelpers.GetReturnObject(null, false, "Could not update bus"));
                }
                else
                    return BadRequest(WebHelpers.GetReturnObject(null, false, $"Could not update bus. BusType should either be {BusType.Economy} or {BusType.Executive}"));
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
                var busToDelete = await _busRepository.GetAsync<Bus>(busId);
                if (busToDelete == null)
                    return NotFound(WebHelpers.GetReturnObject(null, false, "Bus could not be found. Please delete an existing bus!"));

                await _dbContext.BusTickets.Where(x => x.BusId == busId).Include(x => x.Bus).ForEachAsync(x => x.Bus = null);
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
