using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Api.Serialization;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;


namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> logger;
        private readonly IInventoryService inventoryService;

        public InventoryController(ILogger<InventoryController> _logger, IInventoryService _inventoryService)
        {
            logger = _logger;
            inventoryService = _inventoryService;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetCurrentInventory()
        {
            logger.LogInformation("Getting all Inventory ...");
            var inventory = inventoryService.GetCurrentInventory()
                .Select(pi => new ProductInventoryModel
                {
                    Id = pi.Id,
                 //   Product = ProductMapper.SerializeProductModel(pi.Product),
                    IdealQuantity = pi.IdealQuantity,
                    QuantityOnHand = pi.QuantityOnHand
                })
              //  .OrderBy(inv => inv.Product.Name)
                .ToList();
            return Ok(inventory);
        }

        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            logger.LogInformation(
                "Updating inventory " +
                $" for {shipment.ProductId} - " +
                $" Adjustment {shipment.Adjustment}");
            var id = shipment.ProductId;
            var adjustment = shipment.Adjustment;
            var inventory = inventoryService.UpdateUnitsAvailable(id, adjustment);
            return Ok(inventory);
        }

        [HttpGet("/api/inventory/snapshot")]
        public ActionResult GetSnapShotHistory()
        {
            logger.LogInformation("Getting all snapshot history");
            try
            {
                var snapShotHistory = inventoryService.GetSnapshotHistory();
                // get distinct points in timeline was collected
                var timeLineMarkers = snapShotHistory.Select(i => i.SnapShotTime).Distinct().ToList();
                // get qunatities grouped by id
                var snapshots = snapShotHistory.GroupBy(hist => hist.Product, hist => hist.QuantityOnHand,
                    (key, g) => new ProductInventorySnapshotModel
                    {
                        ProductId = key.Id,
                        QuantityOnHand = g.ToList()
                    }).OrderBy(hist => hist.ProductId)
                    .ToList();

                var viewModel = new SnapshotResponse
                {
                    TimeLine = timeLineMarkers,
                    ProductInventorySnapshots = snapshots

                };
                return Ok(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Error getting snapshot history");
                logger.LogError(e.StackTrace);
                return BadRequest("Error Retrieving History");
            }
        }
    }
}