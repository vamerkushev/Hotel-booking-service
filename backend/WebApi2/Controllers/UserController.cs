using Application.DTOs.User;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

[ApiController]
[Route( "app/" )]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    
    public UserController( UserService userService )
    {
        _userService = userService;
    }

    [HttpGet( "profile/{id:guid}" )]
    public IActionResult GetUser( [FromRoute] Guid id )
    {
        UserDto user = _userService.GetUser( id );
        
        return Ok( user );
    }
}
