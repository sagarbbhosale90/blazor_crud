using BlazorOAuthAPI.Model;
using BlazorOAuthAPI.Repository;

namespace BlazorOAuthAPI.QueryTypes
{
    public class ProductQueryTypes
    {
        [GraphQLName("getByProductId")]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Products?> GetProductsByIdAsync([Service] IProductRepository product, int productId)
        {
            return await product.GetProductByIdAsync(productId);
        }

        [GraphQLName("GetAllProducts")]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Products>> GetAllProductsAsync([Service] IProductRepository product)
        {
            return await product.GetAllProductsAsync();
        }

        [GraphQLName("getByProductName")]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Products?> GetProductsByNameAsync([Service] IProductRepository product, string productName)
        {
            return await product.GetProductByNameAsync(productName);
        }
    }
}
