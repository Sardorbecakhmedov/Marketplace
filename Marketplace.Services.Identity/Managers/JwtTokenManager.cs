using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Marketplace.Common.Options;
using Marketplace.Services.Identity.Entities;
using Marketplace.Services.Identity.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Marketplace.Services.Identity.Managers;

public class JwtTokenManager : IJwtManager
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenManager(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string CreateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_jwtOptions.SecretKey));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var utcNow = DateTime.UtcNow;

        var jwtSecurity = new JwtSecurityToken
        (
            issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: claims,
            signingCredentials: signingCredentials,
            notBefore: utcNow,
            expires: utcNow.AddMinutes(_jwtOptions.ExpiresMinutes)
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurity);

        return jwtToken;
    }
}