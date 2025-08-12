using BlazorOAuthAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorOAuthAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IDbContextFactory<ProductDbContext> _DbFactory { get; set; }
        private ProductDbContext _DbContext;
        public ProductRepository(IDbContextFactory<ProductDbContext> DbFactory, ProductDbContext dbContext)
        {
            _DbFactory = DbFactory;
            _DbContext = _DbFactory.CreateDbContext();
        }
        public async Task<Products> AddProductAsync(Products product)
        {
            try
            {
                await _DbContext.Products.AddAsync(product);
                var result = await _DbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return product;
                }
                else
                {
                    return product;
                }
            }
            catch (Exception ex)
            {
                return product;
            }
        }

        public async Task<Products> DeleteProductAsync(int productId)
        {
            var findProduct = _DbContext.Products.Where(_ => _.ProductId == productId).FirstOrDefault();
            if (findProduct != null)
            {
                _DbContext.Products.Remove(findProduct);
                var result = await _DbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return findProduct;
                }
                else
                {
                    return findProduct;
                }
            }

            return findProduct;
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            return await _DbContext.Products.ToListAsync();
        }

        public async Task<Products?> GetProductByIdAsync(int productId)
        {
            return await _DbContext.Products.FirstOrDefaultAsync(_ => _.ProductId == productId);
        }

        public async Task<Products?> GetProductByNameAsync(string productName)
        {
            return await _DbContext.Products.FirstOrDefaultAsync(_ => _.ProductName == productName);
        }

        public async Task<Products> UpdateProductAsync(Products product)
        {
            try
            {
                _DbContext.Products.Update(product);
                var result = await _DbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return product;
                }
                else
                {
                    return product;
                }
            }
            catch (Exception ex)
            {
                return product;
            }
        }
    }
}
