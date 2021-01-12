using BusBookingApp.Data;
using BusBookingApp.PayStackApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.PayStackApi.Repositories
{
    public interface ITransactionRepository
    {
        Task<ResponseObject<TransactionInitializationResponseData>> InitiatePayment(string amount);
        Task<ResponseObject<TransactionVerificationResponseData>> VerifyPayment(string reference);
        string CreateTransactionReference();
        User GetCurrentUser();
    }
}
