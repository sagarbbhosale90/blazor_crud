using BlazorOAuthAPI.Model;

namespace BlazorOAuthAPI.Repository
{
    public interface IProductRepository
    {
        Task<Products> AddProductAsync(Products product);
        Task<Products> UpdateProductAsync(Products product);
        Task<Products> DeleteProductAsync(int productId);
        Task<Products?> GetProductByIdAsync(int productId);
        Task<IEnumerable<Products>> GetAllProductsAsync();
        Task<Products?> GetProductByNameAsync(string productName);
    }
}
