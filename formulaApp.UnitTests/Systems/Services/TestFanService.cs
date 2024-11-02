using FluentAssertions;
using FormualApp.Api.Configuration;
using FormualApp.Api.Domains;
using FormualApp.Api.Services;
using FormualApp.Api.Services.Interfaces;
using formulaApp.UnitTests.Fixtures;
using formulaApp.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace formulaApp.UnitTests.Systems.Services;

public class TestFanService
{
    [Fact]
    public async Task GetAllFans_OnInvoke_HttpGet()
    {
        //Arrange
        const string _url = "http://localhost:5000/api";
        var response = FansFixture.GetFans();
        var mockHandler = MockHttpHandler<Fan>.SetupGetRequest(response);
        var httpClient = new HttpClient(mockHandler.Object);
        var config = Options.Create(new ApiServiceConfig()
        {
            Url = _url
        });
        var fanService = new FanService(httpClient, config);
        //Act
        await fanService.GetAll();
        //Assert
        mockHandler.Protected().Verify("SendAsync", Times.Once(),
            ItExpr.Is<HttpRequestMessage>(h => h.Method == HttpMethod.Get && h.RequestUri.ToString() == _url),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetAllFans_OnInvoke_ListOfFans()
    {
        //Arrange
        const string _url = "http://localhost:5000/api";
        var response = FansFixture.GetFans();
        var mockHandler = MockHttpHandler<Fan>.SetupGetRequest(response);
        var httpClient = new HttpClient(mockHandler.Object);
        var config = Options.Create(new ApiServiceConfig()
        {
            Url = _url
        });
        var fanService = new FanService(httpClient, config);
        //Act
        var result = await fanService.GetAll();
        //Assert
        result.Should().BeOfType<List<Fan>>();
    }
    [Fact]
    public async Task GetAllFans_OnInvoke_ReturnEmptyList()
    {
        //Arrange
        const string _url = "http://localhost:5000/api";
        var mockHandler = MockHttpHandler<Fan>.SetupReturnNotFound();
        var httpClient = new HttpClient(mockHandler.Object);
        var config = Options.Create(new ApiServiceConfig()
        {
            Url = _url
        });
        var fanService = new FanService(httpClient, config);
        //Act
        var result = await fanService.GetAll();
        //Assert
        result.Count.Should().Be(0);
    }
    [Fact]
    public async Task GetAllFans_OnInvoke_ReturnNull()
    {
        //Arrange
        const string _url = "http://localhost:5000/api";
        var mockHandler = MockHttpHandler<Fan>.SetupReturnUnAuthorized();
        var httpClient = new HttpClient(mockHandler.Object);
        var config = Options.Create(new ApiServiceConfig()
        {
            Url = _url
        });
        var fanService = new FanService(httpClient, config);
        //Act
        var result = await fanService.GetAll();
        //Assert
        result.Should().BeNull();
    }
}