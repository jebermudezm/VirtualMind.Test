using VirtualMind.Test.Library.Model;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfUsers()
    {
        // Arrange
        var users = new List<User>
            {
                new User { Id = 1, Name = "Test1" },
                new User { Id = 2, Name = "Test2" }
            }.AsQueryable();
        _mockUserService.Setup(svc => svc.Get()).ReturnsAsync(users);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(okResult);
    }

    [Fact]
    public async Task GetById_ReturnsUser()
    {
        // Arrange
        var userId = 1;
        var mockUser = new User { Id = userId, Name = "John" };
        _mockUserService.Setup(service => service.GetById(userId)).ReturnsAsync(mockUser);

        // Act
        var result = await _controller.GetById(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mockUser, okResult.Value);
    }

    [Fact]
    public async Task GetById_NoSuchUser_ReturnsOkWithNullValue()
    {
        // Arrange
        var userId = 2;
        _mockUserService.Setup(service => service.GetById(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.GetById(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Null(okResult.Value);
    }

    [Fact]
    public async Task Post_NullUser_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.Post(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Update_NullUser_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.Update(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.Delete(-1);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

}
