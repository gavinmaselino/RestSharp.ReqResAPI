using System.Text.Json;
using RestSharp.ReqResAPI.Models;

namespace RestSharp.ReqResAPI;

public class ApiMethods
{
    private  RestClient Client { get; set; }
    private  RestRequest Request { get; set; }
    
    private  RestResponse Response { get; set; }
    
    public ApiMethods InitialiseEndpoint(string url, string resource, string operation)
    {
        Client = new RestClient(url);
        var methodType = operation.ToLower() switch
        {
            "get" => Method.Get,
            "post" => Method.Post,
            _ => Method.Get
        };
        Request = new RestRequest(resource, methodType);
        return this;
    }

    public ApiMethods AddRequestBody(ReqResParams parameters)
    {
        Request.AddJsonBody(parameters);
        return this;
    }

    public RestResponse ExecuteRequest()
    { 
        Request.RequestFormat = DataFormat.Json;
        Response = Client.Execute(Request);

        return Response;
    }
    
    public T ExecuteRequest<T>()
    {
        Request.RequestFormat = DataFormat.Json;
        Response = Client.Execute<T>(Request);
        
        var jsonResponseStatus = Response.StatusDescription;
        
        T? jsonResponseContent = default;
        jsonResponseContent = jsonResponseStatus == "OK" ? JsonSerializer.Deserialize<T>(Response.Content) : jsonResponseContent;

        return jsonResponseContent;
    }
}