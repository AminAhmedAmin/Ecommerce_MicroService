using Basket.Application.Mapper;
using Basket.Application.Queries;
using Discount.Grpc.Protos;
using Basket.Application.GrpcServices;
using Basket.Infrastructure.GrpcServices;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();


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


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
builder.Services.AddSwaggerGen();

// Grpc Configuration
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});
builder.Services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
