using Xunit;

namespace Poster.IntegrationTests;

public class UserControllerTests : 
    IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UserControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task UnauthorizedAccess_ReturnsUnauthorized()
    {
        // TODO
    }

    public async Task GetUserWithGoodInput_ReturnsUserInfo()
    {
        // TODO
    }

    public async Task GetUserWithNotExistingId_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task GetUsersWithGoodInput_ReturnsUsers()
    {
        // TODO
    }

    public async Task GetUsersWithNegativeOffserOrLimit_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task GetFollowersCountByIdWithGoodInput_ReturnsCount()
    {
        // TODO
    }

    public async Task GetFollowersCountByIdWithNotExistingUserId_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task GetFollowersCountByCookie_ReturnsCount()
    {
        // TODO
    }

    public async Task GetFollowersCountByWrongCookie_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task FollowUserWithGoodInput_ReturnsNoContent()
    {
        // TODO
    }

    public async Task FollowUserWithNotExistingUserId_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task FollowUserWithCallerUserId_ReturnsBadRequest()
    {
        // TODO
    }
    
    public async Task UnFollowUserWithGoodInput_ReturnsNoContent()
    {
        // TODO
    }

    public async Task UnFollowUserWithNotExistingUserId_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task UnFollowUserWithCallerUserId_ReturnsBadRequest()
    {
        // TODO
    }
    

    public void Dispose()
    {
        
    }
}