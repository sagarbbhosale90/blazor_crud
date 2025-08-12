using BlazorApp_Crud.Model;
using BlazorApp_Crud.Repository;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Crud.Components.Pages.ProductsPages
{
    public partial class Delete : ComponentBase
    {
        private Products? products;

        [SupplyParameterFromQuery]
        private int ProductId { get; set; }

        [Inject]
        public required IServiceProvider ServiceProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [SupplyParameterFromQuery(Name = "datasource")]
        public required string ProductDataSource { get; set; }

        public IProductRepository ProductRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Enum.TryParse<DataSourceEnum>(ProductDataSource, out var dataSourceEnum))
            {
                ProductRepository = ServiceProvider.GetRequiredKeyedService<IProductRepository>(dataSourceEnum);
            }

            products = await ProductRepository.GetProductByIdAsync(ProductId);

            if (products is null)
            {
                NavigationManager.NavigateTo("notfound");
            }
        }

        private async Task DeleteProducts()
        {
            await ProductRepository.DeleteProductAsync(ProductId);

            NavigationManager.NavigateTo("/products");
        }
    }
}
