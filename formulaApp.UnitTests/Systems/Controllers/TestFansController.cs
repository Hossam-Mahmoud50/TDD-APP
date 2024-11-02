using FluentAssertions;
using FormualApp.Api.Controllers;
using FormualApp.Api.Domains;
using FormualApp.Api.Services.Interfaces;
using formulaApp.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace formulaApp.UnitTests.Systems.Controllers;

public class TestFansController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        //Arrange
        var mockFansService = new Mock<IFanService>();
        mockFansService.Setup(x => x.GetAll()).ReturnsAsync(FansFixture.GetFans());
        var fansController = new FansController(mockFansService.Object);
        //Act
        var result = (OkObjectResult)await fansController.Get();
        //Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeOfType<List<Fan>>();
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeService()
    {
        //Arrange
        var mockFansService = new Mock<IFanService>();
        mockFansService.Setup(x => x.GetAll()).ReturnsAsync(FansFixture.GetFans());
        var fansController = new FansController(mockFansService.Object);
        //Act
        var result = (OkObjectResult)await fansController.Get();
        //Assert
        mockFansService.Verify(x => x.GetAll(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnListOfFans()
    {
        //Arrange
        var mockFansService = new Mock<IFanService>();
        mockFansService.Setup(x => x.GetAll()).ReturnsAsync(new List<Fan>()
        {
            new Fan() { Id = 1, Email = "hossam@gmail.com", Name = "hossam" },
        });
        var fansController = new FansController(mockFansService.Object);
        //Act
        var result = (OkObjectResult)await fansController.Get();
        //Assert
        result.Should().BeOfType<OkObjectResult>();
        result.Value.Should().BeOfType<List<Fan>>();
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnNotFound()
    {
        //Arrange
        var mockFansService = new Mock<IFanService>();
        mockFansService.Setup(x => x.GetAll()).ReturnsAsync(FansFixture.GetFans());
        var fansController = new FansController(mockFansService.Object);
        //Act
        var result = (NotFoundResult)await fansController.Get();
        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}