using System;
using System.Collections.Generic;
using System.Text;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.product;

namespace SolarCoffee.Services.Order
{
    public interface IOrderService
    {
        List<SalesOrder> GetOrders();
        ServiceResponse<bool> GenerateOpenOrder(SalesOrder order);
        ServiceResponse<bool> MarkFulfilled(int id);
    }
}
