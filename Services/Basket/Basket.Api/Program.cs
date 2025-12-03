using Basket.Application.Mapper;
using Basket.Application.Queries;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(BasketMappingProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, Assembly.GetAssembly(typeof(GetBasketQuery))));

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
// Register Basket repository
builder.Services.AddScoped<Basket.Core.Repositories.IBasketRepository, Basket.Infrastructure.Repositories.BasketRepository>();


builder.Services.AddSwaggerGen(

// options =>

//options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//{
//    Title = "Catalog.API",
//    Version = "v1", 
//    Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
//} 
);
var app = builder.Build();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
