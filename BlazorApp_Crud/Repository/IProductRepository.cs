using BlazorApp_Crud.Model;

namespace BlazorApp_Crud.Repository
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(Products product);
        Task<bool> UpdateProductAsync(Products product);
        Task<bool> DeleteProductAsync(int productId);
        Task<Products?> GetProductByIdAsync(int productId);
        Task<Products?> GetProductByNameAsync(string productName);
        Task<IEnumerable<Products>> GetAllProductsAsync();
        IQueryable<Products> GetAllProductsQueryable();
        Products? GetProductByName(string productName);
    }
}
