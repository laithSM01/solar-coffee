﻿using System;
using System.Collections.Generic;
using System.Text;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.product;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory();
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventory GetByProductId(int productId);
        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}
