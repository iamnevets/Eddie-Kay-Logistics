using System;
using System.Threading.Tasks;
using BusBookingApp.Data.Models;
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
        public async Task<ActionResult> InitiatePayment(BusTicket ticket)
        {
            try
            {
                var amount = ticket.Bus.Price;
                if (!string.IsNullOrEmpty(amount))
                {
                    var transactionResponse = await _transactionRepository.InitiatePayment(amount);
                    return Ok(WebHelpers.GetReturnObject(transactionResponse.Data, transactionResponse.Status, transactionResponse.Message));
                }
                else
                    return BadRequest(WebHelpers.GetReturnObject(null, false, "Failed to initiate payment. Price is required"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }

        [HttpGet("{reference}")]
        public async Task<ActionResult> VerifyPayment(string reference)
        {
            try
            {
                if (!string.IsNullOrEmpty(reference))
                {
                    var verificationResponse = await _transactionRepository.VerifyPayment(reference);
                    var currentUser = _transactionRepository.GetCurrentUser();
                    PaymentTransaction transactionModel = new PaymentTransaction
                    {
                        TransactionReference = _transactionRepository.CreateTransactionReference(),
                        Amount = verificationResponse.Data.Amount,
                        UserId = currentUser.Id,
                        Name = currentUser.Name,
                        UserName = currentUser.UserName,
                        Email = currentUser.Email,
                        PhoneNumber = currentUser.PhoneNumber,
                        StudentId = currentUser.StudentId,
                        TransactionStatus = verificationResponse.Data.Status
                    };
                    _generalRepository.Add(transactionModel);

                    if(await _generalRepository.SaveChangesAsync())
                    {
                        return Ok(WebHelpers.GetReturnObject(verificationResponse.Data, verificationResponse.Status, verificationResponse.Message));
                    }
                    else
                        return BadRequest(WebHelpers.GetReturnObject(null, false, "Failed to verify payment. Transaction could not be saved."));
                }
                else return BadRequest(WebHelpers.GetReturnObject(null, false, "Failed to verify payment. Payment reference is required !"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, WebHelpers.ProcessException(e));
            }
        }
    }

    public class TransactionInitializationRequestBody
    {
        public string CallbackUrl { get; set; }
        public string Amount { get; set; }
    }
}
