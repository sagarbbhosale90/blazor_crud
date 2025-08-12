using BlazorApp_Crud.Model;
using BlazorApp_Crud.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp_Crud.Components.Pages.ProductsPages
{
    public partial class Create : ComponentBase
    {
        [SupplyParameterFromForm]
        private Products Products { get; set; }

        [Parameter]
        public required string ProductDataSource { get; set; }

        public IProductRepository? ProductRepository { get; set; }

        [Inject]
        public required IServiceProvider ServiceProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private ValidationMessageStore? messageStore;
        private EditContext? editContext;

        protected override void OnInitialized()
        {
            if (!Enum.TryParse<DataSourceEnum>(ProductDataSource, out var dataSourceEnum))
            {
                throw new InvalidOperationException($"Invalid data source: {ProductDataSource}");
            }

            ProductRepository = ServiceProvider.GetRequiredKeyedService<IProductRepository>(dataSourceEnum);


            Products ??= new Products()
            {
                ProductName = string.Empty,
                Price = 0,
                ProductId = 0,
                Quantity = 0
            };

            editContext = new(Products);
            editContext.OnValidationRequested += HandleValidationRequested;
            editContext.OnFieldChanged += (sender, args) =>
            {
                // Clear validation messages when a field changes
                messageStore?.Clear(args.FieldIdentifier);
            };

            messageStore = new(editContext);
        }

        private void HandleValidationRequested(object? sender,
                    ValidationRequestedEventArgs args)
        {
            messageStore?.Clear();

            var product = ProductRepository.GetProductByName(Products.ProductName);

            // Custom validation logic
            if (product != null)
            {
                messageStore?.Add(() => Products.ProductName, "Product Name alredy exists.");
            }
        }

        // To protect from overposting attacks, see https://learn.microsoft.com/aspnet/core/blazor/forms/#mitigate-overposting-attacks.
        private async Task AddProducts()
        {
            // Run built-in validation
            bool isValid = editContext.Validate();

            if (isValid)
            {
                await ProductRepository.AddProductAsync(Products);

                NavigationManager.NavigateTo("/products/" + ProductDataSource);
            }
        }

        private void BackToList()
        {
            NavigationManager.NavigateTo("/products/" + ProductDataSource);
        }
    }
}
