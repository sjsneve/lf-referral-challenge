using System.Net;
using System.Text.Json;
using CartonCaps.Api.Models;

namespace TrainingApi.Tests;

public class IntegrationTests
{
    [Fact]
    public async Task GET_AccountInviteFriends_ReturnsInviteFriendsModel()
    {
        // Arrange
        var app = new ApiApplication();

        // Act
        var client = app.CreateClient();
        var response = await client.GetAsync("/account/invitefriends");
        var responseBody = await response.Content.ReadAsStringAsync();
        InviteFriendsModel? inviteFriendsModel =  JsonSerializer.Deserialize<InviteFriendsModel>(responseBody, JsonSerializerOptions.Web);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("ABC123", inviteFriendsModel.ReferralCode);
    }
}