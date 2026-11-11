using Application.DTOs.User;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

[ApiController]
[Route( "api/users" )]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController( UserService userService )
    {
        _userService = userService;
    }

    [HttpGet( "{id:guid}" )]
    public IActionResult GetUser( [FromRoute] Guid id )
    {
        UserDto user = _userService.GetUser( id );

        return Ok( user );
    }
}
