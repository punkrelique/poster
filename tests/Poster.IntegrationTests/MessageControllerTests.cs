using Xunit;

namespace Poster.IntegrationTests;

public class MessageControllerTests 
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public MessageControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task UnauthorizedAccess_ReturnsUnauthorized()
    {
        // TODO
    }

    public async Task GetFollowingUsersMessagesWithGoodInput_ReturnsMessages()
    {
        // TODO
    }

    public async Task GetFollowingUsersMessagesWithNegativeOffsetOrLimit_ReturnsBadRequest()
    {
        // TODO
    }

    public async Task PostMessage_CreatesMessage()
    {
        // TODO
    }

    public async Task PostMessage_WithWrongUserIdReturnsBadRequest()
    {
        // TODO
    }

    public async Task DeleteMessage_DeletesMessage()
    {
        // TODO
    }

    public async Task DeleteMessageNotAuthoredByUser_ReturnsBadRequest()
    {
        // TODO
    }
}