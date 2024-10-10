using Refit;

namespace DotnetRefitApi;

/// <summary>
/// Defines the API endpoints for blog-related operations.
/// </summary>
public interface IBlogApi
{
    /// <summary>
    /// Retrieves all blog posts asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A task representing a collection of posts.</returns>
    [Get("/posts")]
    Task<IEnumerable<Post>> GetAllPostsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a single post by its ID.
    /// </summary>
    /// <param name="postId">The ID of the post to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A task representing the post object.</returns>
    [Get("/posts")]
    Task<Post> GetPostByIdAsync([Query] int postId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new blog post asynchronously.
    /// </summary>
    /// <param name="post">The post data to create.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A task representing the created post.</returns>
    [Post("/posts")]
    Task<Post> CreatePostAsync([Body] Post post, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing post asynchronously.
    /// </summary>
    /// <param name="postId">The ID of the post to update.</param>
    /// <param name="post">The updated post data.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A task representing the updated post.</returns>
    [Put("/posts/{postId}")]
    Task<Post> UpdatePostAsync(int postId, [Body] Post post, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a post by its ID asynchronously.
    /// </summary>
    /// <param name="postId">The ID of the post to delete.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A task representing the deletion process.</returns>
    [Delete("/posts/{postId}")]
    Task DeletePostAsync(int postId, CancellationToken cancellationToken = default);
}

