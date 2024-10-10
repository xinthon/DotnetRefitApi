using System.Text.Json.Serialization;

namespace DotnetRefitApi;

public class Post
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("body")]
    public required string Body { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}
