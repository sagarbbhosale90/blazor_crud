using BlazorOAuthAPI.Model;
using BlazorOAuthAPI.Mutations;
using BlazorOAuthAPI.QueryTypes;
using BlazorOAuthAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContextFactory<ProductDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQLServer()
                .AddQueryType<ProductQueryTypes>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .AddMutationType<ProductsMutation>();
                

builder.Services.AddScoped<IProductRepository, ProductRepository>();    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();
 
app.Run();
