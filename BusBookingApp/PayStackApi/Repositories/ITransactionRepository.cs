using BusBookingApp.PayStackApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.PayStackApi.Repositories
{
    public interface ITransactionRepository
    {
        Task<ResponseObject<TransactionInitializationResponseData>> InitiatePayment(PaymentTransaction transactionModel);
        Task<ResponseObject<TransactionVerificationResponseData>> VerifyPayment(string reference);
    }
}
