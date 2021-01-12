using BusBookingApp.Data;
using BusBookingApp.PayStackApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BusBookingApp.PayStackApi.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly User _currentUser;

        public TransactionRepository(ApplicationDbContext dbContext, IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = GetCurrentUser();
        }

        public User GetCurrentUser()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);

            return currentUser;
        }

        public string CreateTransactionReference()
        {
            var time = DateTime.UtcNow.ToString("hh:mm:ss").Replace(":", "");
            var date = DateTime.UtcNow.Date.ToShortDateString().Replace("/", "");
            Console.WriteLine(date);

            var lastThreeDigitsOfPhoneNumber = _currentUser.PhoneNumber.Substring(6, 3);

            var reference = $"GH-{date}-{time}-{lastThreeDigitsOfPhoneNumber}";
            return reference;
        }

        public async Task<ResponseObject<TransactionInitializationResponseData>> InitiatePayment(decimal amount)
        {
            amount *= 100;
            var transactionReference = CreateTransactionReference();

            var values = new Dictionary<string, string>
            {
                {"email", _currentUser.Email },
                {"amount", amount.ToString()},
                {"reference", transactionReference }
            };

            var client = _clientFactory.CreateClient("paystack");
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://api.paystack.co/transaction/initialize", content);

            ResponseObject<TransactionInitializationResponseData> returnObject = null;
            if (response.IsSuccessStatusCode)
            {
                returnObject = await response.Content.ReadFromJsonAsync<ResponseObject<TransactionInitializationResponseData>>();
            }
            else
            {
                returnObject.Message = response.ReasonPhrase;
            }

            return returnObject;
        }

        public async Task<ResponseObject<TransactionVerificationResponseData>> VerifyPayment(string reference)
        {
            var client = _clientFactory.CreateClient("paystack");
            client.DefaultRequestHeaders.Remove("Accept");
            var response = await client.GetAsync("https://api.paystack.co/transaction/verify/" + reference);

            ResponseObject<TransactionVerificationResponseData> returnObject = null;
            if (response.IsSuccessStatusCode)
            {
                returnObject = await response.Content.ReadFromJsonAsync<ResponseObject<TransactionVerificationResponseData>>();
            }
            else
            {
                returnObject.Message = response.ReasonPhrase;
            }

            return returnObject;
        }
    }
}
