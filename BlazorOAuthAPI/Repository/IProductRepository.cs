using BlazorOAuthAPI.Model;

namespace BlazorOAuthAPI.Repository
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(Products product);
        Task<bool> UpdateProductAsync(Products product);
        Task<bool> DeleteProductAsync(int productId);
        Task<Products?> GetProductByIdAsync(int productId);
        Task<IEnumerable<Products>> GetAllProductsAsync();
    }
}
