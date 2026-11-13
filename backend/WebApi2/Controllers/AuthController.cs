using Application.DTOs.Auth;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

[ApiController]
[Route( "api/auth" )]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController( AuthService authService )
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost( "register" )]
    public IActionResult Register( [FromBody] RegisterRequestDto request )
    {
        AuthResponseDto response = _authService.Register( request );

        return Ok( response );
    }

    [AllowAnonymous]
    [HttpPost( "login" )]
    public IActionResult Login( [FromBody] LoginRequestDto request )
    {
        AuthResponseDto response = _authService.Login( request );

        return Ok( response );
    }

    [AllowAnonymous]
    [HttpPost( "refresh" )]
    public IActionResult Refresh( [FromBody] RefreshTokenRequestDto request )
    {
        AuthResponseDto response = _authService.Refresh( request );

        return Ok( response );
    }

    [AllowAnonymous]
    [HttpPost( "logout" )]
    public IActionResult Logout( [FromBody] RefreshTokenRequestDto request )
    {
        _authService.Logout( request );

        return NoContent();
    }
}
