//using BusBookingApp.PayStackApi.Repositories;
//using Microsoft.Extensions.Logging;
//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BusBookingApp.Scheduler
//{
//    [DisallowConcurrentExecution]
//    public class PaymentVerificationJob : IJob
//    {
//        private readonly ILogger<PaymentVerificationJob> _logger;
//        private readonly ITransactionRepository _transactionRepository;

//        public PaymentVerificationJob(ILogger<PaymentVerificationJob> logger, ITransactionRepository transactionRepository)
//        {
//            _logger = logger;
//            _transactionRepository = transactionRepository;
//        }

//        public Task Execute(IJobExecutionContext context)
//        {
//            try
//            {
//                _logger.LogInformation("Starting payment verification");
//                var verifyPayment = _transactionRepository.VerifyPayment();
//                return Task.FromResult(verifyPayment);
//            }
//            catch (Exception e)
//            {
//                _logger.LogInformation(e.Message);
//            }

//            return Task.FromResult(0);
//        }
//    }
//}
