using Domain.Enums;

namespace Domain.Entities;

public class Booking
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public decimal Total { get; private set; }
    public CurrencyList Currency { get; private set; }
    public int UserCount { get; private set; }
    public DateOnly BookingDate { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public User? User { get; private set; }
    public ICollection<RoomInBooking> RoomInBookings { get; private set; } = new List<RoomInBooking>();

    private Booking()
    {
    }

    public Booking( int userId, int userCount, CurrencyList currency, DateOnly bookingDate )
    {
        UserId = userId;
        UserCount = userCount;
        Currency = currency;
        BookingDate = bookingDate;
        Total = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetTotal( decimal total )
    {
        Total = total;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update( int userCount, CurrencyList currency )
    {
        UserCount = userCount;
        Currency = currency;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}