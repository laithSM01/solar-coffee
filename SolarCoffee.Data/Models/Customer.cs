using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } // customer first created
        public DateTime UpdateOn { get; set; } // customer last updated
        public string firstName { get; set; }
        public string lastName { get; set; }

        public CustomerAdresses MainAdresses { get; set; }
    }
}
