using AutoMapper;
using MCA.Services.ProductAPI.DbContexts;
using MCA.Services.ProductAPI.Models;
using MCA.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace MCA.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository( ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

         public async Task<ProductDto> CreateUpdateProductAsync(ProductDto productDto)
        {
            Product product=_mapper.Map<ProductDto, Product>(productDto);
            if (product.ProductId>0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            return  _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProductByIdAsync(int productId)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Product product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (product==null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            List<Product> products = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
