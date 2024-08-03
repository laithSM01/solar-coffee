using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.product
{
    public interface IProductService
    {
        //what behavior should a product service have ?
        List<Data.Models.Product> GetAllProducts(); //ability to get all products
        Data.Models.Product GetProductById(int id);  //get one product
        ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product);
        ServiceResponse<Data.Models.Product> ArchiveProduct(int id);
    }
}
