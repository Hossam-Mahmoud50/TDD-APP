using System.Net;
using System.Net.Http.Headers;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace formulaApp.UnitTests.Helpers;

public class MockHttpHandler<T>
{
    //Success
    internal static Mock<HttpMessageHandler> SetupGetRequest(List<T> response)
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(response))
        };
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        return mockHandler;
    }
    
    //NotFound 
    internal static Mock<HttpMessageHandler> SetupReturnNotFound()
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent(string.Empty)
        };
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        return mockHandler;
    }
    //NotFound 
    internal static Mock<HttpMessageHandler> SetupReturnUnAuthorized()
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized)
        {
            Content = new StringContent(string.Empty)
        };
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        return mockHandler;
    }
}