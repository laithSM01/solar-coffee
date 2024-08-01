using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SolarCoffee.Data.Models
{
    public class CustomerAdresses
    {
        //[MaxLength(100)] data attribute  Entity Frame work communicate with sql , lets put max limit to these fields to certain length
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn {  set; get; }

        [MaxLength(100)] 
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State {  get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(100)] 
        public string Country { get; set; }
    }
}
