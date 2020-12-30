using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.PayStackApi
{
    public class ResponseObject<T> where T : class
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

}
