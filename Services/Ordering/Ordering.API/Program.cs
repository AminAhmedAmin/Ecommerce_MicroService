using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Middleware;
using Ordering.Application.Behaviors;
using Ordering.Application.Handlers;
using Ordering.Application.Mapper;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;
using Ordering.API.Extensions;
using Ordering.API.EventBus;
using System.Reflection;

using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(SeriLogger.Configure);

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

// FluentValidation Configuration
builder.Services.AddValidatorsFromAssembly(typeof(CheckoutOrderHandler).Assembly);

// MediatR Pipeline Behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Repository Configuration
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Event Bus Consumer Configuration
builder.Services.AddScoped<BasketCheckoutConsumer>();
builder.Services.AddScoped<BasketCheckoutConsumerV2>();

var eventBusHostName = builder.Configuration["EventBusSettings:HostName"] ?? "localhost";
var eventBusExchangeName = builder.Configuration["EventBusSettings:ExchangeName"] ?? "basket_checkout_exchange";
var eventBusQueueName = builder.Configuration["EventBusSettings:QueueName"] ?? "basket_checkout_queue";

// V2 Configuration
var eventBusExchangeNameV2 = builder.Configuration["EventBusSettings:ExchangeNameV2"] ?? "basket_checkout_exchange_v2";
var eventBusQueueNameV2 = builder.Configuration["EventBusSettings:QueueNameV2"] ?? "basket_checkout_queue_v2";

builder.Services.AddHostedService(sp => new RabbitMQConsumerService(
    eventBusHostName,
    eventBusExchangeName,
    eventBusQueueName,
    sp,
    sp.GetRequiredService<ILogger<RabbitMQConsumerService>>()));

builder.Services.AddHostedService(sp => new RabbitMQConsumerServiceV2(
    eventBusHostName,
    eventBusExchangeNameV2,
    eventBusQueueNameV2,
    sp,
    sp.GetRequiredService<ILogger<RabbitMQConsumerServiceV2>>()));

var app = builder.Build();

// Run migrations and seed database
app.MigrateDatabase<OrderContext>(OrderContextSeed.Seed);

// Exception Handling Middleware (should be early in the pipeline)
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
