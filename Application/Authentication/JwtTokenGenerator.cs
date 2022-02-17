using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Authentication;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly AuthenticationSettings _authenticationSettings;

    public JwtTokenGenerator(AuthenticationSettings authenticationSettings)
    {
        _authenticationSettings = authenticationSettings;
    }

    public string GetTokenStr(User user)
    {
        var claims = GetClaims(user);
        var token = GenerateToken(claims);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        return new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("Email", user.Email),
            new Claim("Nick", user.Nick)
        };
    }

    private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtIssuer));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays);

        return new(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            signingCredentials: signingCredentials,
            expires: expires);
    }
}
