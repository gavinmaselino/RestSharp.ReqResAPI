using NUnit.Framework;
using RestSharp.ReqResAPI.Models;

namespace RestSharp.ReqResAPI.Tests;

[TestFixture]
public class ApiTests
{
    private ApiMethods api;
    private ReqResParams rqParams;
    private const string BaseUrl = "https://reqres.in/api/";
    
    [Test]
    public void RegisteringAUserInTheCorrectFormat_Returns200()
    {
        const string resource = "register";
        const string operation = "POST";
        rqParams = new ReqResParams()
        {
            Email = "eve.holt@reqres.in",
            Password = "pistol"
        };
        
        api = new ApiMethods();
        var response = api.InitialiseEndpoint(BaseUrl, resource, operation)
            .AddRequestBody(rqParams)
            .ExecuteRequest();
        
        var numericStatusCode = (int)response.StatusCode;
        Assert.That(numericStatusCode, Is.EqualTo(200));
    }
    
    [Test]
    public void LoginAsAuthorisedUser_ReturnsOK()
    {
        const string resource = "login";
        const string operation = "POST";
        rqParams = new ReqResParams()
        {
            Email = "eve.holt@reqres.in",
            Password = "cityslicka"
        };
        
        api = new ApiMethods();
        var response = api.InitialiseEndpoint(BaseUrl, resource, operation)
            .AddRequestBody(rqParams)
            .ExecuteRequest();
            
        Assert.That(response.StatusDescription, Is.EqualTo("OK"));
    }
    
    [Test]
    public void FetchOfResourceList_Returns_SecondObjectInArray_With_IdOf2_And_ColorOfC74375()
    {
        const string resource = "unknown";
        const string operation = "GET";
        
        const int expectedId = 2;
        const string expectedColor = "#C74375";

        api = new ApiMethods();
        var response = 
            api.InitialiseEndpoint(BaseUrl, resource, operation)
            .ExecuteRequest<Resource>();
        
        var secondDataId = response.Data[1].Id;
        var secondDataColor = response.Data[1].Color;

        Assert.Multiple(() =>
        {
            Assert.That(secondDataId, Is.EqualTo(expectedId));
            Assert.That(secondDataColor, Is.EqualTo(expectedColor));
        });
    }
    
    [Test]
    public void FetchingUsersEndpoint_Returns_KeepAliveHeader()
    {
        const string resource = "users";
        const string operation = "GET";

        api = new ApiMethods();
        var response = api.InitialiseEndpoint(BaseUrl, resource, operation)
            .ExecuteRequest();
        
        var valConnection = response.Headers.FirstOrDefault(h => h.Name == "Connection").Value;
        
        Assert.That(valConnection, Is.EqualTo("keep-alive"));
    }
}