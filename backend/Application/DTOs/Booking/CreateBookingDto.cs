namespace Application.DTOs.Booking;

public class CreateBookingDto
{
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly DepartureDate { get; set; }
    public int UserCount { get; set; }
}