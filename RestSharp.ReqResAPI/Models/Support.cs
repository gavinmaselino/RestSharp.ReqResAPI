using System.Text.Json.Serialization;

namespace RestSharp.ReqResAPI.Models;

public class Support
{
    [JsonPropertyName("url")]
    public Uri? Url { get; set; }
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}