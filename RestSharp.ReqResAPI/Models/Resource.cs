using System.Text.Json.Serialization;

namespace RestSharp.ReqResAPI.Models;

public class Resource
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
    [JsonPropertyName("total")]
    public int Total { get; set; }
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
    
    [JsonPropertyName("data")]
    public List<Data>? Data { get; set; }
    [JsonPropertyName("support")]
    public Support? Support { get; set; }
    
}