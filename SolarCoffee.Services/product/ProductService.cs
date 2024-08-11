using System;
using System.Collections.Generic;
using System.Linq; // build various type of query for collections, array/list of things
using System.Text;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.product
{
    public class ProductService : IProductService
    {
        private readonly SolardbContext _db;

        /* frame work going to say: i request for solardbContext , 
         * and ill be able to pass that through our consturctor here
         * so no where in our app should see like  productservice = new productservice
         * framework handle mapping the instance of new SolardbContext with the dbContext  
         *  that isn't use in any class that depend on it
         */
        public ProductService(SolardbContext dbContext) { 
        _db = dbContext;
        }
        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }
        public Data.Models.Product GetProductById(int id) 
        { return _db.Products.Find(id); }
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);
                // newInventory record for Product
                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10,
                };
                // we added it in Inventories table for tracking
                _db.ProductInventorys.Add(newInventory);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new Product",
                    IsSuccess = true
                };
            }
            catch (Exception ex) {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    //Message = "Error saving new Product",
                    Message = ex.StackTrace,
                    IsSuccess = false
                };
            }
        }
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {

            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Product Archived",
                    IsSuccess = true
                };
            }
            catch (Exception ex) {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = ex.StackTrace,
                    IsSuccess = true
                };
            }
           
          
        }
    }
}
