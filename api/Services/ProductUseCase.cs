using common;
namespace api.Services;

public class ProductUseCase
{
    private readonly IProductRepository _productRepository;

    public ProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync() => await _productRepository.GetAllProductsAsync();

    public async Task<Product> GetProductAsync(int id) => await _productRepository.GetProductByIdAsync(id);

    public async Task<Product> CreateProductAsync(Product product) => await _productRepository.CreateProductAsync(product);

    public async Task<bool> UpdateProductAsync(Product product) => await _productRepository.UpdateProductAsync(product);

    public async Task<bool> DeleteProductAsync(int id) => await _productRepository.DeleteProductAsync(id);
}
