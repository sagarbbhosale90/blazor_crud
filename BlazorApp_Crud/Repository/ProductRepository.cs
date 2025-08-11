using BlazorApp_Crud.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp_Crud.Repository
{
    public class ProductRepository : IProductRepository, IAsyncDisposable
    {
        private IDbContextFactory<ProductDbContext> _DbFactory { get; set; }
        protected ProductDbContext _DbContext;
        public ProductRepository(IDbContextFactory<ProductDbContext> DbFactory, ProductDbContext dbContext)
        {
            _DbFactory = DbFactory;
            _DbContext = _DbFactory.CreateDbContext();
        }
        public async Task<bool> AddProductAsync(Products product)
        {
            try
            {
                await _DbContext.Products.AddAsync(product);
                var result = await _DbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var findStudentData = _DbContext.Products.Where(_ => _.ProductId == productId).FirstOrDefault();
            if (findStudentData != null)
            {
                _DbContext.Products.Remove(findStudentData);
                var result = await _DbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            return await _DbContext.Products.ToListAsync();
        }

        public Task<Products?> GetProductByIdAsync(int productId)
        {
            return _DbContext.Products.FirstOrDefaultAsync(_ => _.ProductId == productId);
        }

        public async Task<bool> UpdateProductAsync(Products product)
        {
            _DbContext.Products.Update(product);
            var result = await _DbContext.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public IQueryable<Products> GetAllProductsQueryable()
        {
            return _DbContext.Products.AsQueryable();
        }

        public async ValueTask DisposeAsync()
        {
            await _DbContext.DisposeAsync();
        }

        public async Task<Products?> GetProductByNameAsync(string productName)
        {
            return await _DbContext.Products.FirstOrDefaultAsync(_ => _.ProductName == productName);
        }

        public Products? GetProductByName(string productName)
        {
            return _DbContext.Products.FirstOrDefault(_ => _.ProductName == productName);
        }
    }
}
