namespace Domain.Entities;

public class RoomCategory
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int MinUserCount { get; private set; }
    public int MaxUserCount { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public ICollection<Room> Rooms { get; private set; } = new List<Room>();
    public ICollection<RatePlanRoomCategory> RatePlanRoomCategories { get; private set; } = new List<RatePlanRoomCategory>();

    private RoomCategory()
    {
    }

    public RoomCategory( string name, int minUserCount, int maxUserCount )
    {
        Name = name;
        MinUserCount = minUserCount;
        MaxUserCount = maxUserCount;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update( string name, int minUserCount, int maxUserCount )
    {
        Name = name;
        MinUserCount = minUserCount;
        MaxUserCount = maxUserCount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}