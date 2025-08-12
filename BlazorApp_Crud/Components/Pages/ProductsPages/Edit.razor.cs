using BlazorApp_Crud.Model;
using BlazorApp_Crud.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp_Crud.Components.Pages.ProductsPages
{
    public partial class Edit : ComponentBase
    {
        [SupplyParameterFromQuery]
        private int ProductId { get; set; }

        [SupplyParameterFromForm]
        private Products? Products { get; set; }

        [Inject]
        public required IServiceProvider ServiceProvider { get; set; }

        public IProductRepository ProductRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [SupplyParameterFromQuery(Name = "datasource")]
        public required string ProductDataSource { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (Enum.TryParse<DataSourceEnum>(ProductDataSource, out var dataSourceEnum))
            {
                ProductRepository = ServiceProvider.GetRequiredKeyedService<IProductRepository>(dataSourceEnum);
            }
            
            Products ??= await ProductRepository.GetProductByIdAsync(ProductId);

            if (Products is null)
            {
                NavigationManager.NavigateTo("notfound");
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://learn.microsoft.com/aspnet/core/blazor/forms/#mitigate-overposting-attacks.
        private async Task UpdateProducts()
        {
            try
            {
                await ProductRepository.UpdateProductAsync(Products!);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(Products!.ProductId))
                {
                    NavigationManager.NavigateTo("notfound");
                }
                else
                {
                    throw;
                }
            }

            NavigationManager.NavigateTo("/products");
        }

        private bool ProductsExists(int productid)
        {
            return ProductRepository.GetProductByIdAsync(productid) is not null;
        }
    }
}
