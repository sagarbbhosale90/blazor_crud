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

        public Task<bool> AddProductAsync(Products product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            var response = await httpClient.PostAsJsonAsync("graphql", QueryForGrpahQl.GetAllProduct);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch products from GraphQL API.");
            }

            return await response.Content.ReadFromJsonAsync<List<Products>>() ?? [];
        }

        public IQueryable<Products> GetAllProductsQueryable()
        {
            var response = httpClient.PostAsJsonAsync("graphql", QueryForGrpahQl.GetAllProduct).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                throw new MyGrpahQlExcepection("Failed to fetch products from GraphQL API.");
            }

            GraphQLResponse<string>? graphQLResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (graphQLResponse?.Errors != null && graphQLResponse.Errors.Length > 0)
            {
                // handle GraphQL errors
                throw new MyGrpahQlExcepection(
                    string.Join("; ", graphQLResponse.Errors.Select(e => e.Message))
                );
            }


            return result.Data?.AsQueryable();
        }

        public Task<Products?> GetProductByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Products? GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<Products?> GetProductByNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Products product)
        {
            throw new NotImplementedException();
        }
    }
}
