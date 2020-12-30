using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Data
{
    public class ResponseObject<T> where T : class
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ResponseData
    {
        public string Authorization_url { get; set; }
        public string Access_code { get; set; }
        public string Reference { get; set; }
    }

}
