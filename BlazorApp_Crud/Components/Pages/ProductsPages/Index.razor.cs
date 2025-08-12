using BlazorApp_Crud.Model;
using BlazorApp_Crud.Repository;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Crud.Components.Pages.ProductsPages
{
    public partial class Index : ComponentBase
    {
       
        [Parameter]
        public required string ProductDataSource { get; set; }

        [SupplyParameterFromQuery(Name = "datasource")]
        public required string ProductDataSource2 { get; set; }

        public required IProductRepository ProductRepository { get; set; }

        [Inject]
        public required IServiceProvider ServiceProvider { get; set; }

        protected override void OnInitialized()
        {
            if (!Enum.TryParse<DataSourceEnum>(ProductDataSource, out var dataSourceEnum))
            {
                _ = Enum.TryParse<DataSourceEnum>(ProductDataSource2, out dataSourceEnum);

                ProductDataSource = ProductDataSource2;
            }

            ProductRepository = ServiceProvider.GetRequiredKeyedService<IProductRepository>(dataSourceEnum);
        }
    }
}
