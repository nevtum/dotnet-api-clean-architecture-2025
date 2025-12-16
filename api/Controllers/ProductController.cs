using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // Thread-safe in-memory collection for demonstration
        private static ConcurrentDictionary<int, Product> _products = new();
        private static int _nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products.Values);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            if (_products.TryGetValue(id, out var product))
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            product.Id = Interlocked.Increment(ref _nextId);
            _products[product.Id] = product;
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            if (!_products.ContainsKey(id))
            {
                return NotFound();
            }

            product.Id = id;
            _products[id] = product;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (_products.TryRemove(id, out _))
            {
                return NoContent();
            }
            return NotFound();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
