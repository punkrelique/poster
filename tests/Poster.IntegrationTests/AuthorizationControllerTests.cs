using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Poster.Infrastructure;
using Poster.IntegrationTests.Utils;
using Xunit;

namespace Poster.IntegrationTests;

public class AuthorizationControllerTests 
    : IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public AuthorizationControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Registration_WithGoodInput_ReturnsNoContent()
    {
        // Arrange
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", "registrationTest"), 
            new KeyValuePair<string, string>("Email", "registrationTest@mail.ru"), 
            new KeyValuePair<string, string>("Password", "registrationTest!123")
        });

        // Act
        var response = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task Registration_WithAlreadyExistingMail_ReturnsBadRequest()
    {
        // Arrange
        var formContentLeft = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", "newUserTest"), 
            new KeyValuePair<string, string>("Email", "newRegistrationTest@mail.ru"), 
            new KeyValuePair<string, string>("Password", "registrationTest!123")
        });
        
        var formContentRight = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", "newUserTest123"), 
            new KeyValuePair<string, string>("Email", "newRegistrationTest@mail.ru"), 
            new KeyValuePair<string, string>("Password", "registrationTest!123")
        });

        // Act
        var responseLeft = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContentLeft);
        
        var responseRight = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContentRight);
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, responseLeft.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, responseRight.StatusCode);
    }
    
    [Fact]
    public async Task Registration_WithUserAlreadyExistingUsername_ReturnsBadRequest()
    {
        // Arrange
        var formContentLeft = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", "veryNewUser"), 
            new KeyValuePair<string, string>("Email", "newRegistrationTestUsername@mail.ru"), 
            new KeyValuePair<string, string>("Password", "registrationTest!123")
        });
        
        var formContentRight = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", "veryNewUser"), 
            new KeyValuePair<string, string>("Email", "veryNewRegistrationTestUsername2222@mail.ru"), 
            new KeyValuePair<string, string>("Password", "registrationTest!123")
        });

        // Act
        var responseLeft = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContentLeft);
        
        var responseRight = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContentRight);
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, responseLeft.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, responseRight.StatusCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("Testing!!!")]
    [InlineData("Тестирую")]
    [InlineData("Testing%@$")]
    public async Task Registration_WithBadUsername_ReturnsBadRequest(
        string username)
    {
        // Arrange
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", username), 
            new KeyValuePair<string, string>("Email", "yandex1@google.ru"), 
            new KeyValuePair<string, string>("Password", "safe_password!2")
        });
        
        // Act
        var response = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public async Task Registration_WithVeryLongUsername_ReturnsBadRequest()
    {
        // Arrange
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", Constants.VeryLongString), 
            new KeyValuePair<string, string>("Email", "yandex1@google.ru"), 
            new KeyValuePair<string, string>("Password", "safe_password!2")
        });
        
        // Act
        var response = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("Testing!!@.ru!")]
    [InlineData("Тестирую@.ru")]
    [InlineData("Testing%@$")]
    [InlineData("!!!!@yandex.ru")]
    [InlineData("1@gmail.")]
    [InlineData("what??b")]
    [InlineData("@com")]
    [InlineData("fMyMail!!!@gmail.ru")]
    [InlineData("fgg$@ya.ru")]
    [InlineData("fgg#@ya.ru")]
    [InlineData("fgg?@ya.ru")]
    [InlineData("fg__g?@ya.ru")]
    [InlineData("mymail@ru")]
    public async Task Registration_WithBadEmail_ReturnsBadRequest(string email)
    {
        // Arrange
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", "normal_name"), 
            new KeyValuePair<string, string>("Email", email), 
            new KeyValuePair<string, string>("Password", "safe_password!2")
        });
        
        // Act
        var response = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public async Task Registration_WithVeryLongEmail_ReturnsBadRequest()
    {
        // Arrange
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Username", "normal_name"), 
            new KeyValuePair<string, string>("Email", Constants.VeryLongString), 
            new KeyValuePair<string, string>("Password", "safe_password!2")
        });
        
        // Act
        var response = await _client.PostAsync(
            "/api/v1/Authorization/Registration",
            formContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public async Task Registration_WithBadPassword_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task Login_WithBadLoginCredentials_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task Login_WithGoodLoginCredentials_ReturnsBadRequest()
    {
        // TODO
    }
    
    // Clear all created users in order to isolate tests
    public void Dispose()
    {
        using var scope = _factory.Services.GetService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetService<IApplicationDbContext>();
        foreach (var user in context.Users)
            context.Users.Remove(user);

        context.SaveChanges();
    }
}