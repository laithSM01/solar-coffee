using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Api.Serialization;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    /// <summary>
    /// Handles mapping Order Data to and from releated View models
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Maps an InvoiceModel view model to a SalesOrder Data model
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static SalesOrder SerializeInvoiceOrder(InvoiceModel invoice)
        {
            var now = DateTime.UtcNow;
            var salesOrderItem = invoice.LineItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();
            return new SalesOrder
            {
                SalesOrderItems = salesOrderItem,
                CreatedOn = now,
                UpdatedOn = now
            };
        }

        /// <summary>
        /// Maps a collection of sales order to order models
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            //we are mapping from collection of sales orders to collection of order models
            return orders.Select(
                order => new OrderModel
                {
                    Id = order.Id,
                    CreatedOn = order.CreatedOn,
                    UpdatedOn = order.UpdatedOn,
                    SalesOrderItems = SerializesSalesOrderItem(order.SalesOrderItems),
                    Customer = CustomerMapper.SerializeCustomer(order.Customer),
                    IsPaid = order.IsPaid
                }).ToList();

        }

        /// <summary>
        /// Maps a collection of SalesOrderItems (data) to SalesOrderItemModels (view models)
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializesSalesOrderItem(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(
                item => new SalesOrderItemModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();
        }
    }
}