using Microsoft.AspNetCore.Mvc;
using Moq;
using api.Services;
using common;

namespace api.Controllers.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductUseCase> _mockProductUseCase;
        private ProductController _productController;

        [SetUp]
        public void Setup()
        {
            _mockProductUseCase = new Mock<IProductUseCase>();
            _productController = new ProductController(_mockProductUseCase.Object);
        }

        [Test]
        public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.00M },
                new Product { Id = 2, Name = "Product 2", Price = 20.00M }
            };
            _mockProductUseCase.Setup(useCase => useCase.GetProductsAsync()).ReturnsAsync(products);

            var actionResult = await _productController.GetProducts();

            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<List<Product>>());
            var returnProducts = okResult.Value as List<Product>;
            Assert.That(returnProducts, Is.Not.Null);
            Assert.That(returnProducts.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetProduct_ReturnsOkResult_WithValidId()
        {
            var productId = 1;
            var product = new Product { Id = productId, Name = "Product 1", Price = 10.00M };
            _mockProductUseCase.Setup(useCase => useCase.GetProductAsync(productId)).ReturnsAsync(product);

            var actionResult = await _productController.GetProduct(productId);

            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<Product>());
            var returnProduct = okResult.Value as Product;
            Assert.That(returnProduct, Is.Not.Null);
            Assert.That(returnProduct.Id, Is.EqualTo(productId));
        }

        [Test]
        public async Task GetProduct_ReturnsNotFoundResult_WithInvalidId()
        {
            var invalidProductId = 99;
            _mockProductUseCase.Setup(useCase => useCase.GetProductAsync(invalidProductId)).ReturnsAsync((Product?)null);

            var actionResult = await _productController.GetProduct(invalidProductId);

            Assert.That(actionResult.Result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
