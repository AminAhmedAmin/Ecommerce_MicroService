using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(
   // options =>

//options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//{
//    Title = "Catalog.API",
//    Version = "v1",
//    Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
//} 
);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  //  app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
