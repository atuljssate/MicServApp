using MCA.Services.ProductAPI.Models.Dto;

namespace MCA.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();       
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<ProductDto> CreateUpdateProductAsync(ProductDto productDto);
        Task <bool> DeleteProductByIdAsync(int productId);

    }
}
