using Application.DTOs.User;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi2.Authorization;

namespace WebApi2.Controllers;

[ApiController]
[Authorize]
[Route( "api/users" )]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController( UserService userService )
    {
        _userService = userService;
    }

    [Authorize( Roles = Roles.Admin )]
    [HttpPost( "get" )]
    public IActionResult GetUser( [FromBody] GetUserRequestDto request )
    {
        UserDto user = _userService.GetUser( request.UserId );

        return Ok( user );
    }
}
