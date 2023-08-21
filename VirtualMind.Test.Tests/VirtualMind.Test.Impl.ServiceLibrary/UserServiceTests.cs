
using VirtualMind.Test.Library.Model;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepoMock.Object);
    }

    [Fact]
    public async Task TestCreateUser()
    {
        // Arrange
        User newUser = new User { Id = 1, Name = "UserTest"};
        _userRepoMock.Setup(m => m.Create(newUser)).ReturnsAsync(true);

        // Act
        var result = await _userService.Create(newUser);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task TestDeleteUser()
    {
        // Arrange
        int userId = 1;
        _userRepoMock.Setup(m => m.Delete(userId)).ReturnsAsync(true);

        // Act
        var result = await _userService.Delete(userId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task TestGetUsers()
    {
        // Arrange
        var users = new List<User>
            {
                new User { Id = 1, Name = "Test" }
            }.AsQueryable();

        _userRepoMock.Setup(repo => repo.Get()).ReturnsAsync(users);

        // Act
        var result = await _userService.Get();

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result?.FirstOrDefault()?.Id ?? 0);
    }

    [Fact]
    public async Task TestGetUserById()
    {
        // Arrange
        int userId = 1;
        var user = new User { Id = 1, Name = "UserTest" };
        _userRepoMock.Setup(m => m.GetById(userId)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetById(userId);

        // Assert
        Assert.Equal(userId, result.Id);
    }

    [Fact]
    public async Task TestUpdateUser()
    {
        // Arrange
        User updateUser = new User { Id = 1, Name = "UserTest" };
        _userRepoMock.Setup(m => m.Update(updateUser)).ReturnsAsync(true);

        // Act
        var result = await _userService.Update(updateUser);

        // Assert
        Assert.True(result);
    }
}
