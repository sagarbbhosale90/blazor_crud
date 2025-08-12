using BlazorApp_Crud.Model;

namespace BlazorApp_Crud.Repository
{
    public class GrpahQLProductRepository : IProductRepository
    {
        public IHttpClientFactory _HttpClientFactory { get; set; }
        public HttpClient httpClient { get; set; }

        public GrpahQLProductRepository(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
            httpClient = _HttpClientFactory.CreateClient("GraphQLClient");
        }

        public async Task<bool> AddProductAsync(Products product)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("graphql", QueryForGrpahQl.BuildAddProductRequest(product));

                if (!response.IsSuccessStatusCode)
                {
                    throw new MyGrpahQlExcepection("Failed to fetch products from GraphQL API.");
                }

                var graphQLResponse = GraphQLResult.HandleGraphQLResponse<Products>(await response.Content.ReadAsStringAsync(), "addProduct");

                if (graphQLResponse?.Errors != null && graphQLResponse.Errors.Count > 0)
                {
                    // handle GraphQL errors
                    throw new MyGrpahQlExcepection(
                        string.Join("; ", graphQLResponse.Errors.Select(e => e))
                    );
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("graphql", QueryForGrpahQl.BuildDeleteProductRequest(productId));

                if (!response.IsSuccessStatusCode)
                {
                    throw new MyGrpahQlExcepection("Failed to fetch products from GraphQL API.");
                }

                var graphQLResponse = GraphQLResult.HandleGraphQLResponse<Products>(await response.Content.ReadAsStringAsync(), "getByProductId");

                if (graphQLResponse?.Errors != null && graphQLResponse.Errors.Count > 0)
                {
                    // handle GraphQL errors
                    throw new MyGrpahQlExcepection(
                        string.Join("; ", graphQLResponse.Errors.Select(e => e))
                    );
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Products> GetAllProductsQueryable()
        {
            var response = httpClient.PostAsJsonAsync("graphql", QueryForGrpahQl.GetAllProduct).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                throw new MyGrpahQlExcepection("Failed to fetch products from GraphQL API.");
            }

            var graphQLResponse = GraphQLResult.HandleGraphQLResponse<List<Products>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), "GetAllProducts");

            if (graphQLResponse?.Errors != null && graphQLResponse.Errors.Count > 0)
            {
                // handle GraphQL errors
                throw new MyGrpahQlExcepection(
                    string.Join("; ", graphQLResponse.Errors.Select(e => e))
                );
            }

            return graphQLResponse?.Data?.AsQueryable() ?? new List<Products>().AsQueryable();
        }

        public async Task<Products?> GetProductByIdAsync(int productId)
        {
            var response = await httpClient.PostAsJsonAsync("graphql", QueryForGrpahQl.GetProductById(productId));

            if (!response.IsSuccessStatusCode)
            {
                throw new MyGrpahQlExcepection("Failed to fetch products from GraphQL API.");
            }

            var graphQLResponse = GraphQLResult.HandleGraphQLResponse<Products>(await response.Content.ReadAsStringAsync(), "getByProductId");

            if (graphQLResponse?.Errors != null && graphQLResponse.Errors.Count > 0)
            {
                // handle GraphQL errors
                throw new MyGrpahQlExcepection(
                    string.Join("; ", graphQLResponse.Errors.Select(e => e))
                );
            }

            return graphQLResponse?.Data;
        }

        public async Task<Products?> GetProductByNameAsync(string productName)
        {
            var response = await httpClient.PostAsJsonAsync("graphql", QueryForGrpahQl.GetProductByName(productName));

            if (!response.IsSuccessStatusCode)
            {
                throw new MyGrpahQlExcepection("Failed to fetch products from GraphQL API.");
            }

            var graphQLResponse = GraphQLResult.HandleGraphQLResponse<Products>(await response.Content.ReadAsStringAsync(), "getByProductName");

            if (graphQLResponse?.Errors != null && graphQLResponse.Errors.Count > 0)
            {
                // handle GraphQL errors
                throw new MyGrpahQlExcepection(
                    string.Join("; ", graphQLResponse.Errors.Select(e => e))
                );
            }

            return graphQLResponse?.Data;
        }


        public async Task<bool> UpdateProductAsync(Products product)
        {
            try
            {

                var request = new
                {
                    query = @"
        mutation UpdateProduct($product: ProductsInput!) {
            updateProduct(productDetails: $product) {
                productId
                productName
                price
                quantity
            }
        }
    ",
                    variables = new
                    {
                        product = product
                    }
                };

                var response = await httpClient.PostAsJsonAsync("graphql", request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new MyGrpahQlExcepection("Failed to fetch products from GraphQL API.");
                }

                var graphQLResponse = GraphQLResult.HandleGraphQLResponse<Products>(await response.Content.ReadAsStringAsync(), "updateProduct");

                if (graphQLResponse?.Errors != null && graphQLResponse.Errors.Count > 0)
                {
                    // handle GraphQL errors
                    throw new MyGrpahQlExcepection(
                        string.Join("; ", graphQLResponse.Errors.Select(e => e))
                    );
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        Products? IProductRepository.GetProductByName(string productName)
        {
            var response = httpClient.PostAsJsonAsync("graphql", QueryForGrpahQl.GetProductByName(productName)).ConfigureAwait(false).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                throw new MyGrpahQlExcepection("Failed to fetch products from GraphQL API.");
            }

            var graphQLResponse = GraphQLResult.HandleGraphQLResponse<Products>(response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult(), "getByProductName");

            if (graphQLResponse?.Errors != null && graphQLResponse.Errors.Count > 0)
            {
                // handle GraphQL errors
                throw new MyGrpahQlExcepection(
                    string.Join("; ", graphQLResponse.Errors.Select(e => e))
                );
            }

            return graphQLResponse?.Data;
        }
    }
}
