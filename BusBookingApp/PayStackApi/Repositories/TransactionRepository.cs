using BusBookingApp.PayStackApi;
using BusBookingApp.PayStackApi.Models;
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
        private readonly IHttpClientFactory _clientFactory;

        public TransactionRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ResponseObject<TransactionInitializationResponseData>> InitiatePayment(PaymentTransaction transactionModel)
        {
            var values = new Dictionary<string, string>
            {
                {"email", transactionModel.Email },
                {"amount", transactionModel.Amount }
            };

            var client = _clientFactory.CreateClient("paystack");
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://api.paystack.co/transaction/initialize", content);

            ResponseObject<TransactionInitializationResponseData> returnObject = null;
            if (response.IsSuccessStatusCode)
            {
                //var responseData = await response.Content.ReadFromJsonAsync<ResponseObject<TransactionInitializationResponseData>>();
                returnObject = await response.Content.ReadFromJsonAsync<ResponseObject<TransactionInitializationResponseData>>();
                //returnObject = await VerifyPayment(responseData.Data.Reference);
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
