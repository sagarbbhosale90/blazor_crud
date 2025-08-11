using BlazorOAuthAPI.Model;
using BlazorOAuthAPI.Repository;

namespace BlazorOAuthAPI.Mutations
{
    public class ProductsMutation
    {
        public async Task<bool> AddProductAsync([Service] IProductRepository product, Products productDetails)
        {
            return await product.AddProductAsync(productDetails);
        }

        public async Task<bool> UpdateProductAsync([Service] IProductRepository product, Products productDetails)
        {
            return await product.UpdateProductAsync(productDetails);
        }

        public async Task<bool> DeleteProductAsync([Service] IProductRepository product, int productId)
        {
            return await product.DeleteProductAsync(productId);
        }
    }
}
