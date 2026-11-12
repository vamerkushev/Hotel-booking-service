namespace Application.DTOs.Booking;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly DepartureDate { get; set; }
    public decimal Total { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}