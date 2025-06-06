using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace CartonCaps.IntegrationTests.Mocks;

public static class MockJwtTokens
{
    public static string Issuer { get; } = Guid.NewGuid().ToString(); // random issuer
    public static SecurityKey SecurityKey { get; }
    public static SigningCredentials SigningCredentials { get; }

    private static readonly JwtSecurityTokenHandler TokenHandler = new();
    private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
    private static readonly byte[] Key = new byte[32];

    static MockJwtTokens()
    {
        Rng.GetBytes(Key);
        SecurityKey = new SymmetricSecurityKey(Key) { KeyId = Guid.NewGuid().ToString() };
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
    }

    public static string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        return TokenHandler.WriteToken(new JwtSecurityToken(Issuer, "https://localhost:7265", claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
    }
}