namespace Domain.Entities;

public class Room
{
    public Guid Id { get; private set; }
    public Guid HotelId { get; private set; }
    public Guid RoomCategoryId { get; private set; }
    public string Number { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    private Room()
    {
    }

    public Room( Guid hotelId, Guid roomCategoryId, string number )
    {
        HotelId = hotelId;
        RoomCategoryId = roomCategoryId;
        Number = number;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update( Guid roomCategoryId, string number )
    {
        RoomCategoryId = roomCategoryId;
        Number = number;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}