using common;
using api.Services;
using Moq;

namespace api.Services.Tests
{
    [TestFixture]
    public class ProductUseCaseTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private ProductUseCase _productUseCase;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productUseCase = new ProductUseCase(_productRepositoryMock.Object);
        }

        [Test]
        public async Task GetProductsAsync_ReturnsListOfProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1" },
                new Product { Id = 2, Name = "Test Product 2" }
            };
            _productRepositoryMock.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

            var result = await _productUseCase.GetProductsAsync();

            Assert.That(result, Is.EqualTo(products));
        }

        [Test]
        public async Task GetProductAsync_ValidId_ReturnsProduct()
        {
            var product = new Product { Id = 1, Name = "Test Product" };
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

            var result = await _productUseCase.GetProductAsync(1);

            Assert.That(result, Is.EqualTo(product));
        }

        [Test]
        public async Task CreateProductAsync_ValidProduct_ReturnsCreatedProduct()
        {
            var product = new Product { Id = 1, Name = "Test Product" };
            _productRepositoryMock.Setup(repo => repo.CreateProductAsync(product)).ReturnsAsync(product);

            var result = await _productUseCase.CreateProductAsync(product);

            Assert.That(result, Is.EqualTo(product));
        }

        // Additional tests for UpdateProductAsync and DeleteProductAsync will be added here.
    }
}
