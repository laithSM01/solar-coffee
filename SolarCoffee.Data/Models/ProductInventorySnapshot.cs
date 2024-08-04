using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.Models
{
    /* We are recording the quantityonHand for product for any given point in time
     * and that sense will provide us with auto trail of product inventory over time
    */
    public class ProductInventorySnapshot
    {
        public int Id { get; set; }
        public DateTime SnapShotTime { get; set; }
        public int QuantityOnHand { get; set; }
        public Product Product { get; set; }
    }
}
