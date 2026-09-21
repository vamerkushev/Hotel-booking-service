namespace Domain.Entities;

public class RoomInBooking
{
    public Guid Id { get; private init; }
    public Guid ReservationId { get; private set; }
    public Guid RoomId { get; private set; }
    public DateOnly CheckInDate { get; private set; }
    public DateOnly CheckOutDate { get; private set; }

    public Reservation? Reservation { get; private set; }
    public Room? Room { get; private set; }

    private RoomInBooking()
    {
    }

    public RoomInBooking( Guid reservationId, Guid roomId, DateOnly checkInDate, DateOnly checkOutDate )
    {
        Id = Guid.NewGuid();
        ReservationId = reservationId;
        RoomId = roomId;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }

    public void UpdateDates( DateOnly checkInDate, DateOnly checkOutDate )
    {
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }
}