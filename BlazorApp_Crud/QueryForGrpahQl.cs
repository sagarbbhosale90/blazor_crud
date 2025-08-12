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

        public static object BuildUpdateProductRequestAsJson<T>(T productModel)
        {
            // serialize model into JSON for payload variable
            string payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(productModel);

            return new
            {
                query = @"
            mutation UpdateProduct($payload: String!) {
                updateProductRawJson(productJson: $payload) {
                    productId
                    productName
                    price
                    quantity
                }
            }
        ",
                variables = new
                {
                    payload = productModel
                }
            };
        }

        public static object BuildDeleteProductRequest(int productId)
        {
            return new
            {
                query = @"
            mutation DeleteProduct($productId: Int!) {
                deleteProduct(productId: $productId) {
                    productId
                    price
                    quantity
                    productName
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
