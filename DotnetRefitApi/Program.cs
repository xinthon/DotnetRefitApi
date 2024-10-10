using DotnetRefitApi;
using Microsoft.AspNetCore.Mvc;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Register the Blog API client with Refit and configure the base address
builder.Services.AddRefitClient<IBlogApi>()
    .ConfigureHttpClient(config => config.BaseAddress = 
        new Uri("https://jsonplaceholder.typicode.com"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
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
app.MapGet("posts", async (IBlogApi api) =>
{
    try
    {
        return Results.Ok(await api.GetAllPostsAsync());
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Error fetching all posts");
        return Results.Problem("An error occurred while fetching posts.");
    }
});

app.MapGet("posts/{id:int}", async (int id, IBlogApi api) =>
{
    try
    {
        var post = await api.GetPostByIdAsync(id);
        return post != null ? Results.Ok(post) : Results.NotFound($"Post with id {id} not found.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, $"Error fetching post with id {id}");
        return Results.Problem($"An error occurred while fetching the post with id {id}.");
    }
});

app.MapPost("posts", async ([FromBody] Post post, IBlogApi api) =>
{
    try
    {
        var createdPost = await api.CreatePostAsync(post);
        return Results.Created($"/posts/{createdPost.Id}", createdPost);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Error creating a new post");
        return Results.Problem("An error occurred while creating the post.");
    }
});

app.MapPut("posts/{id:int}", async (int id, [FromBody] Post post, IBlogApi api) =>
{
    try
    {
        var updatedPost = await api.UpdatePostAsync(id, post);
        return Results.Ok(updatedPost);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, $"Error updating post with id {id}");
        return Results.Problem($"An error occurred while updating the post with id {id}.");
    }
});

app.MapDelete("posts/{id:int}", async (int id, IBlogApi api) =>
{
    try
    {
        await api.DeletePostAsync(id);
        return Results.Ok($"Post with id {id} was deleted.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, $"Error deleting post with id {id}");
        return Results.Problem($"An error occurred while deleting the post with id {id}.");
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
