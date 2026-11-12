using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

[ApiController]
[Route( "api/bookings" )]
public class BookingsController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingsController( BookingService bookingService )
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public IActionResult Create( [FromBody] CreateBookingDto dto )
    {
        Guid bookingId = _bookingService.CreateBooking( dto );

        return Ok( bookingId );
    }

    [HttpDelete( "{id:guid}" )]
    public IActionResult Cancel( [FromRoute] Guid id )
    {
        _bookingService.CancelBooking( id );
        return Ok();
    }

    [HttpGet( "user/{userId}" )]
    public IActionResult GetUserBookings( Guid userId )
    {
        IEnumerable<BookingDto> result = _bookingService.GetUserBookings( userId );

        return Ok( result );
    }
}