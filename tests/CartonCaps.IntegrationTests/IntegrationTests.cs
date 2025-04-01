using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using CartonCaps.Api.Models;
using CartonCaps.IntegrationTests.Mocks;

namespace CartonCaps.IntegrationTests;

public class IntegrationTests
{
    [Fact]
    public async Task GET_AccountInviteFriends_ReturnsInviteFriendsModel()
    {
        // Arrange
        var app = new ApiApplication();

        // Act
        var client = app.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            MockJwtTokens.GenerateJwtToken([
                new Claim("sub", "1"),
                new Claim("role", "User")
            ]));

        var response = await client.GetAsync("api/referrals/invite-friends");
        var responseBody = await response.Content.ReadAsStringAsync();
        InviteFriendsModel? inviteFriendsModel = JsonSerializer.Deserialize<InviteFriendsModel>(responseBody, JsonSerializerOptions.Web);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("ABC123", inviteFriendsModel!.ReferralCode);
    }
}