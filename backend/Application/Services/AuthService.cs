using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;

public class AuthService
{
    private const string InvalidCredentialsMessage = "Invalid email or password";
    private const string InvalidRefreshTokenMessage = "Invalid refresh token";

    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService )
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public AuthResponseDto Register( RegisterRequestDto request )
    {
        string email = NormalizeEmail( request.Email );
        if ( _userRepository.FindUserByEmail( email ) != null )
        {
            throw new ConflictException( $"User with email {email} already exists" );
        }

        User user = new(
            request.FirstName.Trim(),
            request.SecondName.Trim(),
            string.IsNullOrWhiteSpace( request.MiddleName ) ? null : request.MiddleName.Trim(),
            email,
            request.Phone.Trim(),
            _passwordHasher.Hash( request.Password ) );

        _userRepository.Save( user );

        return IssueTokens( user );
    }

    public AuthResponseDto Login( LoginRequestDto request )
    {
        User? user = _userRepository.FindUserByEmail( NormalizeEmail( request.Email ) );
        if ( user == null || !_passwordHasher.Verify( user.Password, request.Password ) )
        {
            throw new UnauthorizedException( InvalidCredentialsMessage );
        }

        return IssueTokens( user );
    }

    public AuthResponseDto Refresh( RefreshTokenRequestDto request )
    {
        RefreshToken? refreshToken = _refreshTokenRepository.FindByTokenHash(
            _tokenService.HashRefreshToken( request.RefreshToken ) );

        if ( refreshToken == null )
        {
            throw new UnauthorizedException( InvalidRefreshTokenMessage );
        }

        if ( refreshToken.IsRevoked )
        {
            _refreshTokenRepository.RevokeAllForUser( refreshToken.UserId );
            throw new UnauthorizedException( InvalidRefreshTokenMessage );
        }

        if ( !refreshToken.IsActive( DateTime.UtcNow ) )
        {
            throw new UnauthorizedException( InvalidRefreshTokenMessage );
        }

        refreshToken.Revoke();
        _refreshTokenRepository.Update( refreshToken );

        return IssueTokens( refreshToken.User );
    }

    public void Logout( RefreshTokenRequestDto request )
    {
        RefreshToken? refreshToken = _refreshTokenRepository.FindByTokenHash(
            _tokenService.HashRefreshToken( request.RefreshToken ) );

        if ( refreshToken == null || refreshToken.IsRevoked )
        {
            return;
        }

        refreshToken.Revoke();
        _refreshTokenRepository.Update( refreshToken );
    }

    private AuthResponseDto IssueTokens( User user )
    {
        AccessToken accessToken = _tokenService.GenerateAccessToken( user );

        string refreshTokenValue = _tokenService.GenerateRefreshToken();
        RefreshToken refreshToken = new(
            user.Id,
            _tokenService.HashRefreshToken( refreshTokenValue ),
            _tokenService.GetRefreshTokenExpiration() );

        _refreshTokenRepository.Add( refreshToken );

        return new AuthResponseDto
        {
            AccessToken = accessToken.Token,
            AccessTokenExpiresAt = accessToken.ExpiresAt,
            RefreshToken = refreshTokenValue,
            RefreshTokenExpiresAt = refreshToken.ExpiresAt
        };
    }

    private static string NormalizeEmail( string email )
    {
        return email.Trim().ToLowerInvariant();
    }
}
