using Domain.Enums;

namespace Domain.Entities;

public class Booking
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Total { get; private set; }
    public CurrencyList Currency { get; private set; }
    public int UserCount { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateOnly BookingDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Booking()
    {
    }

    public Booking( Guid userId, int userCount, CurrencyList currency, DateOnly bookingDate, BookingStatus bookingStatus )
    {
        UserId = userId;
        UserCount = userCount;
        Currency = currency;
        BookingDate = bookingDate;
        Total = 0;
        Status = bookingStatus;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetTotal( decimal total )
    {
        Total = total;
    }

    public void Update( int userCount, CurrencyList currency, BookingStatus status )
    {
        UserCount = userCount;
        Currency = currency;
        Status = status;
    }

    public void SetDeleted()
    {
        Status = BookingStatus.CANCELLED;
    }
}