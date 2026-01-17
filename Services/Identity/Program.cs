using Identity.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential(); // Not for production

var app = builder.Build();

app.UseIdentityServer();

app.MapGet("/", () => "Identity Server is running");

app.Run();
