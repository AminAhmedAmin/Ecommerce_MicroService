using Basket.Application.Mapper;
using Basket.Application.Queries;
using Basket.Api.Swagger;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Discount.Grpc.Protos;
using Basket.Application.GrpcServices;
using Basket.Infrastructure.GrpcServices;
using Basket.Infrastructure.EventBus;
using EventBus.Messages.Common;

using System.Reflection;

using Common.Logging;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(SeriLogger.Configure);

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
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://identity.api:8080";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
    });

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
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

// Event Bus Configuration
var eventBusHostName = builder.Configuration["EventBusSettings:HostName"] ?? "localhost";
var eventBusExchangeName = builder.Configuration["EventBusSettings:ExchangeName"] ?? "basket_checkout_exchange";
builder.Services.AddSingleton<IEventBus>(sp => new EventBusService(eventBusHostName, eventBusExchangeName));

// Event Bus V2 Configuration
var eventBusExchangeNameV2 = builder.Configuration["EventBusSettings:ExchangeNameV2"] ?? "basket_checkout_exchange_v2";
builder.Services.AddSingleton<IV2EventBus>(sp => new V2EventBusService(eventBusHostName, eventBusExchangeNameV2));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions;
        foreach (var description in descriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
