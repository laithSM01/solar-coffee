using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.product;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {

        private readonly SolardbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolardbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }
        /// <summary>
        /// Gets a ProductInventory instance by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        /// <summary>
        /// Returns all current inventory from the database
        /// </summary>
        /// <returns></returns>
        List<ProductInventory> IInventoryService.GetCurrentInventory()
        {
            return _db.ProductInventories
               .Include(pi => pi.Product)
               .Where(pi => !pi.Product.IsArchived)
               .ToList();
        }

        /// <summary>
        /// Return Snapshot history for the previous 6 hours
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        List<ProductInventorySnapshot> IInventoryService.GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(2);

            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product) //know snapshot by product
                .Where(snap
                    => snap.SnapShotTime > earliest
                       && !snap.Product.IsArchived) //dont get Archived products
                .ToList();
        }

        /// <summary>
        /// Updates number of units available of the provided product id
        /// Adjusts QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added / removed from inventory</param>
        /// <returns></returns>
        ServiceResponse<ProductInventory> IInventoryService.UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

            //anytime you wanna update UnitsAvailable , you need to create a snapshot to keep auto trail
                try
                {
                    CreateSnapshot();
                }

                catch (Exception e)
                {
                    _logger.LogError("Error creating inventory snapshot.");
                    _logger.LogError(e.StackTrace);
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = DateTime.UtcNow
                };
            }

            catch
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"Error updating ProductInventory QuantityOnHand",
                    Time = DateTime.UtcNow
                };
            }
        }
        /// <summary>
        /// Creates a Snapshot record using the provided ProductInventory instance
        /// </summary>
        private void CreateSnapshot()
        {
            var now = DateTime.UtcNow;

            var inventories = _db.ProductInventories
                .Include(inv => inv.Product)
                .ToList();

            foreach (var inventory in inventories)
            {
                var snapshot = new ProductInventorySnapshot
                {
                    SnapShotTime = now,
                    Product = inventory.Product,
                    QuantityOnHand = inventory.QuantityOnHand
                };

                _db.Add(snapshot);
            }
        }
    }
}
