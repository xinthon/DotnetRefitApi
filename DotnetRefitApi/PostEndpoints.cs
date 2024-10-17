using Microsoft.AspNetCore.Mvc;

namespace DotnetRefitApi;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this WebApplication app)
    {
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
    }
}
