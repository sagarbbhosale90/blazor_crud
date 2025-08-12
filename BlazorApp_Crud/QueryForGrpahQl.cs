namespace BlazorApp_Crud
{
    public static class QueryForGrpahQl
    {
        public static object GetAllProduct = new
        {
            query = @"{
                GetAllProducts {
                    productId
                    productName
                    price
                    quantity
                  }
            }"
        };



        public static object BuildAddProductRequest<T>(T item)
        {
            return new
            {
                query = @"
                            mutation AddProduct($product: ProductsInput!) {
                                addProduct(productDetails: $product) {
                                    productId
                                    productName
                                    price
                                    quantity
                                }
                     }",
                variables = new
                {
                    product = item
                }
            };
        }

        public static object GetProductByName(string productName)
        {
            return new
            {
                query = @"
                query GetByProductName($name: String!) {
                getByProductName(productName: $name) {
                    productId
                    productName
                    price
                    quantity
                }
            }
        ",
                variables = new
                {
                    name = productName
                }
            };
        }

        public static object GetProductById(int productId)
        {
            return new
            {
                query = @"
            query GetByProductId($productId: Int!) {
                getByProductId(productId: $productId) {
                    productId
                    productName
                    price
                    quantity
                }
            }
        ",
                variables = new
                {
                    productId = productId
                }
            };
        }
    }
}
