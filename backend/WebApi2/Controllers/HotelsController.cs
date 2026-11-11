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
    public IActionResult Search( [FromQuery] string city )
    {
        IEnumerable<HotelDto> result = _hotelService.SearchHotels( city );

        return Ok( result );
    }
}