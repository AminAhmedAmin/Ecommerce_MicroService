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
using System.Reflection;

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

// FluentValidation Configuration
builder.Services.AddValidatorsFromAssembly(typeof(CheckoutOrderHandler).Assembly);

// MediatR Pipeline Behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Repository Configuration
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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
