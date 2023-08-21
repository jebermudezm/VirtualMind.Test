public class UserRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _contextOptions;
    private readonly AppDbContext _context;
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
             .UseInMemoryDatabase(databaseName: "TestDatabaseUser")
        .Options;
        _context = new AppDbContext(_contextOptions);
        _repository = new UserRepository(_context);
    }

    [Fact]
    public async Task CreateUser_ReturnsTrue()
    {
        // Arrange
        var user = new User { Name = "Test"};
        // Act
        var result = await _repository.Create(user);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_RemovesUserReturnTrue()
    {
        // Arrange
        var user = new User { Name = "Test" };
        _context.Add(user);
        _context.SaveChanges();
        var id = _context.Users.FirstOrDefault().Id;
        // Act
        var result = await _repository.Delete(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ReturnsFalseIfUserNotFound()
    {
        // Arrange
        var user = new User { Name = "Test" };
        _context.Add(user);
        _context.SaveChanges();
        // Act
        var result = await _repository.Delete(0);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Get_ReturnsAllUsers()
    {
        // Arrange
        var user = new List<User>{ new User { Name = "Test1" }, new User { Name = "Test2" } };
        _context.AddRange(user);
        _context.SaveChanges();
        // Act
        var result = await _repository.Get();

        // Assert

        Assert.Contains(result, u => u.Name == "Test1");
        Assert.Contains(result, u => u.Name == "Test2");
    }

    [Fact]
    public async Task GetById_ReturnsUserIfFound()
    {
        // Arrange
        var user = new User { Name = "Test" };
        _context.Add(user);
        _context.SaveChanges();
        var id = _context.Users.FirstOrDefault().Id;
        // Act
        var result = await _repository.GetById(id);

        // Assert
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNewUserIfNotFound()
    {
        // Arrange
        var user = new User { Name = "Test" };
        _context.Add(user);
        _context.SaveChanges();
        var id = 0;
        // Act
        var result = await _repository.GetById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
    }

    [Fact]
    public async Task Update_ModifiesUserState()
    {
        // Arrange
        var user = new User { Name = "TestUser" };
        _context.Add(user);
        _context.SaveChanges();
        user.Id = _context?.Users?.FirstOrDefault(x => x.Name == "TestUser")?.Id ?? 0;
        user.Name = "TestUser_Updated";
        // Act
        var result = await _repository.Update(user);

        // Assert
        Assert.True(result);
    }
}

