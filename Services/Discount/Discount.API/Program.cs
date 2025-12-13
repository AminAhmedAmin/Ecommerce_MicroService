using Discount.Application.Mapper;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Infrastructure.Data.Contexts;
using Discount.Infrastructure.Repositories;
using Discount.API.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(CouponMappingProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, Assembly.GetAssembly(typeof(GetCouponByIdQuery))));
builder.Services.AddScoped<IDiscountContext, DiscountContext>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();

// Seed database (will be called after app starts)
_ = Task.Run(async () =>
{
    await Task.Delay(5000); // Wait for database to be ready
    await Discount.Infrastructure.Data.Contexts.CouponContextSeed.SeedDataAsync(app.Configuration);
});

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
