namespace Domain.Entities;

public class Room
{
    public Guid Id { get; private init; }
    public Guid PropertyId { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public string Number { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public Property? Property { get; private set; }
    public RoomType? RoomType { get; private set; }
    public ICollection<RoomInBooking> RoomInBookings { get; private set; } = new List<RoomInBooking>();

    private Room()
    {
    }

    public Room( Guid propertyId, Guid roomTypeId, string number, decimal price )
    {
        Id = Guid.NewGuid();
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        Number = number;
        Price = price;
    }

    public void Update( Guid roomTypeId, string number, decimal price )
    {
        RoomTypeId = roomTypeId;
        Number = number;
        Price = price;
    }
}