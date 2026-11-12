using Application.DTOs.General;
using Application.DTOs.Room;
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
    public IActionResult Search( Guid hotelId, [FromQuery] SearchRoomsDto request )
    {
        IEnumerable<RoomDto> result = _roomService.SearchRooms( hotelId, request.Arrival, request.Departure );
        return Ok( result );
    }

    [HttpGet( "/api/rooms/{id:guid}" )]
    public IActionResult GetById( [FromRoute] Guid id )
    {
        RoomDto result = _roomService.GetById( id );
        return Ok( result );
    }
}