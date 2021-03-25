using BusBookingApp.Data;
using BusBookingApp.Data.Models;
using BusBookingApp.PayStackApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusBookingApp.PayStackApi.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly User _currentUser;

        private string _reference = "";
        private bool _paymentInitiatedSuccessfully = false;
        private BusTicket _busTicketCurrentlyBeingBooked = new BusTicket();

        public TransactionRepository(ApplicationDbContext dbContext, IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            //_currentUser = GetCurrentUser();
        }

        public User GetCurrentUser()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);

            return currentUser;
        }

        public string CreateTransactionReference(User currentUser)
        {
            var time = DateTime.UtcNow.ToString("hh:mm:ss").Replace(":", "");
            var date = DateTime.UtcNow.Date.ToShortDateString().Replace("/", "");
            Console.WriteLine(date);

            var lastThreeDigitsOfPhoneNumber = currentUser.PhoneNumber.Substring(6, 3);

            var reference = $"GH-{date}-{time}-{lastThreeDigitsOfPhoneNumber}";
            return reference;
        }

        public async Task<ResponseObject<TransactionInitializationResponseData>> InitiatePayment(decimal price, BusTicket busTicket)
        {
            var currentUser = GetCurrentUser();
            price *= 100;
            var amount = price.ToString("0.00");
            var transactionReference = CreateTransactionReference(currentUser);
            var ticket = JsonSerializer.Serialize(busTicket);

            var values = new Dictionary<string, string>
            {
                {"email", currentUser.Email },
                {"amount", amount},
                {"reference", transactionReference },
                {"custom_fields", ticket }
            };

            var client = _clientFactory.CreateClient("paystack");
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://api.paystack.co/transaction/initialize", content);

            ResponseObject<TransactionInitializationResponseData> returnObject = null;
            if (response.IsSuccessStatusCode)
            {
                returnObject = await response.Content.ReadFromJsonAsync<ResponseObject<TransactionInitializationResponseData>>();

                //Assign the reference from the response to _reference to be used for verification of payment
                _reference = returnObject.Data.Reference;

                //Check if payment initiated successfully, so busTicket can be used in the verifyPayment function
                _paymentInitiatedSuccessfully = true;
                _busTicketCurrentlyBeingBooked = busTicket;
            }
            else
            {
                returnObject.Message = response.ReasonPhrase;
            }

            return returnObject;
        }

        public async Task<ResponseObject<TransactionVerificationResponseData>> VerifyPayment()
        {
            var client = _clientFactory.CreateClient("paystack");
            client.DefaultRequestHeaders.Remove("Accept");
            var response = await client.GetAsync("https://api.paystack.co/transaction/verify/" + _reference);

            ResponseObject<TransactionVerificationResponseData> returnObject;
            if (response.IsSuccessStatusCode)
            {
                returnObject = await response.Content.ReadFromJsonAsync<ResponseObject<TransactionVerificationResponseData>>();

                if(_paymentInitiatedSuccessfully)
                {
                    _busTicketCurrentlyBeingBooked.NewStatus = returnObject.Data.Status;
                }
            }
            else
            {
                returnObject = new ResponseObject<TransactionVerificationResponseData>
                {
                    Message = response.ReasonPhrase
                };
            }

            return returnObject;
        }
    }
}
