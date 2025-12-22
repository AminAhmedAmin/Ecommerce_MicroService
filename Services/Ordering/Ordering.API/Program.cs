using Microsoft.EntityFrameworkCore;
using Ordering.Application.Handlers;
using Ordering.Application.Mapper;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Configuration
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderingConnectionString")));

// AutoMapper Configuration
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<OrderMappingProfile>();
});

// MediatR Configuration
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CheckoutOrderHandler).Assembly));

// Repository Configuration
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
