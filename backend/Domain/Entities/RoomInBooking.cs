namespace Domain.Entities;

public class RoomInBooking
{
    public Guid Id { get; private set; }
    public Guid BookingId { get; private set; }
    public Guid RoomId { get; private set; }
    public DateOnly ArrivalDate { get; private set; }
    public DateOnly DepartureDate { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public Booking? Booking { get; private set; }
    public Room? Room { get; private set; }

    private RoomInBooking()
    {
    }

    public RoomInBooking( int bookingId, int roomId, DateOnly arrivalDate, DateOnly departureDate )
    {
        BookingId = bookingId;
        RoomId = roomId;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDates( DateOnly arrivalDate, DateOnly departureDate )
    {
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}