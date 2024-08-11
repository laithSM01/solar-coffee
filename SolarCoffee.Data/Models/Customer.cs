using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } // customer first created
        public DateTime UpdatedOn { get; set; } // customer last updated
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomerAdresses PrimaryAddress { get; set; }
    }
}
