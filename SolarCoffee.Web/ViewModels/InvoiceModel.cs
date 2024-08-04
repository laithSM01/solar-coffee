using System;
using System.Collections.Generic;
using SolarCoffee.Api.ViewModels;

namespace SolarCoffee.Web.ViewModels
{
    /// <summary>
    /// View Model for open sales order
    /// </summary>
    public class InvoiceModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemModel> LineItems { get; set; }
    }

    /// <summary>
    /// View Model for sales order item model
    /// </summary>
    public class SalesOrderItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductModel Product { get; set; }
    }
}