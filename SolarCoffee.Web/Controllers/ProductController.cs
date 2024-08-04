using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Api.Serialization;
using SolarCoffee.Services.product;

namespace SolarCoffee.Web.Controllers
{
 [ApiController]
    public class ProductController : ControllerBase
    {
      private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;


        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;

        }

        [HttpGet("/api/product")]
        public ActionResult GetProdcut() { // return json
             _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts(); //invoke services
            var productViewModels = products.Select(product =>
            ProductMapper.SerializeProductModel(product));
            return Ok(productViewModels);
        }
        /// <summary>
        /// Archive a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("/api/product/{id}")] //updating the product instance
        public ActionResult ArchiveProduct(int id)
        {
            _logger.LogInformation($"Archiving product {id}");
            var archiveResult = _productService.ArchiveProduct(id);
            return Ok(archiveResult);
        }
    }
}