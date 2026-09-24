namespace Domain.Entities;

public class Hotel
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public int Stars { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public ICollection<Room> Rooms { get; private set; } = new List<Room>();

    private Hotel()
    {
    }

    public Hotel( string name, string country, string city, string street, int stars )
    {
        Name = name;
        Country = country;
        City = city;
        Street = street;
        Stars = stars;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update( string name, string country, string city, string street, int stars )
    {
        Name = name;
        Country = country;
        City = city;
        Street = street;
        Stars = stars;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}