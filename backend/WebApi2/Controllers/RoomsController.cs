using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

[ApiController]
[Route( "api/hotels/{hotelId}/rooms" )]
public class RoomsController : ControllerBase
{
    private readonly RoomService _roomService;

    public RoomsController( RoomService roomService )
    {
        _roomService = roomService;
    }

    [HttpGet]
    public IActionResult Search( Guid hotelId, [FromQuery] DateOnly arrival, [FromQuery] DateOnly departure )
    {
        IEnumerable<RoomDto> result = _roomService.SearchRooms( hotelId, arrival, departure );

        return Ok( result );
    }
}