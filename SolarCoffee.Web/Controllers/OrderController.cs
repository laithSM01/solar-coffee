using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customService;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderService orderService,
            ICustomerService customService)
        {
            _logger = logger;
            _orderService = orderService;
            _customService = customService;
        }

        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Generating Invoice");
            var order = OrderMapper.SerializeInvoiceOrder(invoice);
            order.Customer = _customService.GetCustomerById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok();
        }

        [HttpGet("/api/orders")]
        public ActionResult GetOrders()
        {
            _logger.LogInformation("Get all orders");
            var orders = _orderService.GetOrders();
            var orderViewModel = OrderMapper.SerializeOrdersToViewModels(orders);
            return Ok(orderViewModel);
        }

        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult MarkOrderComplete(int id)
        {
            _logger.LogInformation($"Marking order {id} as complete");
            _orderService.MarkFulfilled(id);
            return Ok();
        }

    }

}