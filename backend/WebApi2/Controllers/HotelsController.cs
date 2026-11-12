using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

[ApiController]
[Route( "api/hotels" )]
public class HotelsController : ControllerBase
{
    private readonly HotelService _hotelService;

    public HotelsController( HotelService hotelService )
    {
        _hotelService = hotelService;
    }

    [HttpGet( "search" )]
    public IActionResult Search( [FromQuery] SearchHotelsDto request )
    {
        IEnumerable<HotelDto> result = _hotelService.SearchHotels( request.City );
        return Ok( result );
    }

    [HttpGet( "{id:guid}" )]
    public IActionResult GetById( [FromRoute] Guid id )
    {
        HotelDto result = _hotelService.GetById( id );
        return Ok( result );
    }
}