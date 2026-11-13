using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using Domain.Interfaces.Models;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Foundation.Security;

public class JwtTokenService : ITokenService
{
    public const string RoleClaimType = "role";

    private const int RefreshTokenBytes = 64;

    private readonly JwtOptions _options;
    private readonly SigningCredentials _signingCredentials;
    private readonly JsonWebTokenHandler _tokenHandler = new();

    public JwtTokenService( IOptions<JwtOptions> options )
    {
        _options = options.Value;
        SymmetricSecurityKey key = new( Encoding.UTF8.GetBytes( _options.SigningKey ) );
        _signingCredentials = new SigningCredentials( key, SecurityAlgorithms.HmacSha256 );
    }

    public AccessToken GenerateAccessToken( User user )
    {
        DateTime now = DateTime.UtcNow;
        DateTime expiresAt = now.AddMinutes( _options.AccessTokenLifetimeMinutes );

        SecurityTokenDescriptor descriptor = new()
        {
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            IssuedAt = now,
            NotBefore = now,
            Expires = expiresAt,
            SigningCredentials = _signingCredentials,
            Claims = new Dictionary<string, object>
            {
                [ JwtRegisteredClaimNames.Sub ] = user.Id.ToString(),
                [ JwtRegisteredClaimNames.Email ] = user.Email,
                [ JwtRegisteredClaimNames.Jti ] = Guid.NewGuid().ToString(),
                [ RoleClaimType ] = user.Role.ToString()
            }
        };

        return new AccessToken( _tokenHandler.CreateToken( descriptor ), expiresAt );
    }

    public string GenerateRefreshToken()
    {
        return Base64UrlEncoder.Encode( RandomNumberGenerator.GetBytes( RefreshTokenBytes ) );
    }

    public string HashRefreshToken( string refreshToken )
    {
        return Convert.ToHexString( SHA256.HashData( Encoding.UTF8.GetBytes( refreshToken ) ) );
    }

    public DateTime GetRefreshTokenExpiration()
    {
        return DateTime.UtcNow.AddDays( _options.RefreshTokenLifetimeDays );
    }
}
