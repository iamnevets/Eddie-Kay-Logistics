using BusBookingApp.Data;
using BusBookingApp.Data.Models;
using BusBookingApp.PayStackApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.PayStackApi.Repositories
{
    public interface ITransactionRepository
    {
        Task<ResponseObject<TransactionInitializationResponseData>> InitiatePayment(decimal amount, BusTicket busTicket);
        Task<ResponseObject<TransactionVerificationResponseData>> VerifyPayment();
        string CreateTransactionReference(User currentUser);
        User GetCurrentUser();
    }
}
