using BlazorOAuthAPI.Model;
using BlazorOAuthAPI.Repository;

namespace BlazorOAuthAPI.Mutations
{
    public class ProductsMutation
    {
        public async Task<Products> AddProductAsync([Service] IProductRepository product, Products productDetails)
        {
            return await product.AddProductAsync(productDetails);
        }

        public async Task<Products> UpdateProductAsync([Service] IProductRepository product, Products productDetails)
        {
            return await product.UpdateProductAsync(productDetails);
        }

        public async Task<Products> DeleteProductAsync([Service] IProductRepository product, int productId)
        {
            return await product.DeleteProductAsync(productId);
        }
    }
}
