using BusBookingApp.PayStackApi.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Scheduler
{
    [DisallowConcurrentExecution]
    public class PaymentVerificationJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentVerificationJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using(var scope = _serviceProvider.CreateScope())
                {
                    Console.WriteLine("+++++ Payment verification job running ... ");

                    var transactionRepo = scope.ServiceProvider.GetService<ITransactionRepository>();
                    await transactionRepo.VerifyPayment();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
