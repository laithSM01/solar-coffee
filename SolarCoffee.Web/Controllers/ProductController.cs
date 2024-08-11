using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Api.Serialization;
using SolarCoffee.Api.ViewModels;
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

        [HttpGet("/api/Product")]
        public ActionResult GetProdcut() { // return json
             _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts(); //invoke services
            var productViewModels = products.Select(product =>
            ProductMapper.SerializeProductModel(product));
            return Ok(productViewModels);
        }
        /// <summary>
        /// Archive a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("/api/Product/{id}")] //updating the Product instance
        public ActionResult ArchiveProduct(int id)
        {
            _logger.LogInformation($"Archiving Product {id}");
            var archiveResult = _productService.ArchiveProduct(id);
            return Ok(archiveResult);
        }
        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("/api/product")]
        public ActionResult AddProduct([FromBody] ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Adding new product");
            var newProduct = ProductMapper.SerializeProductModel(product);
            var newproductResponse = _productService.CreateProduct(newProduct);
            return Ok(newproductResponse);
        }
    }
}