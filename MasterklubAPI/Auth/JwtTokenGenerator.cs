using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Masterklub.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MasterklubAPI.Auth;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerisiToken(Korisnik korisnik, string uloga)
    {
        var claimovi = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, korisnik.Id.ToString()),
            new Claim(ClaimTypes.Email, korisnik.Email),
            new Claim(ClaimTypes.Role, uloga)
        };

        var kljuc = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var kredencijali = new SigningCredentials(kljuc, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claimovi,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: kredencijali);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
