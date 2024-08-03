using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.product
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public T Data { get; set; }
        //client wont be responsible for the format of the res, expect service response :D
    }
}
