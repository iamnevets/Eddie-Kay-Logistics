using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBookingApp.Helpers;
using BusBookingApp.PayStackApi.Models;
using BusBookingApp.PayStackApi.Repositories;
using BusBookingApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingApp.PayStackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(IGeneralRepository generalRepository, ITransactionRepository transactionRepository)
        {
            _generalRepository = generalRepository;
            _transactionRepository = transactionRepository;
        }
        [HttpPost]
        [Route("payment")]
        public async Task<ActionResult> InitiatePayment(PaymentTransaction transactionModel)
        {
            try
            {
                //transactionModel.PaymentTransactionId = Convert.ToInt64(DateTime.UtcNow.Date.ToShortDateString().Replace("/", ""));
                _generalRepository.Add(transactionModel);

                if (await _generalRepository.SaveChangesAsync())
                {
                    var transactionResponse = await _transactionRepository.InitiatePayment(transactionModel);
                    return Ok(WebHelpers.GetReturnObject(transactionResponse.Data, transactionResponse.Status, transactionResponse.Message));
                }
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }
    }
}
