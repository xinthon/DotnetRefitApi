using Microsoft.OpenApi.Models;
using DotnetRefitApi;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AuthHeaderHandler>();

// Register the Blog API client with Refit and configure the base address
builder.Services.AddRefitClient<IBlogApi>()
    .ConfigureHttpClient(config => config.BaseAddress =
        new Uri("https://jsonplaceholder.typicode.com"))
    .AddHttpMessageHandler<AuthHeaderHandler>(); ;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Blog API",
        Description = "A simple API for managing blog posts using Refit",
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API v1");
    });
}

app.UseExceptionHandler("/error");

// Map API endpoints for blog posts
app.MapPostEndpoints();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
