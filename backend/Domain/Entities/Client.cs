namespace Domain.Entities;

public class Client
{
    public Guid Id { get; private init; }
    public string Name { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();

    private Client()
    {
    }

    public Client( string name, string phone )
    {
        Id = Guid.NewGuid();
        Name = name;
        Phone = phone;
    }

    public void Update( string name, string phone )
    {
        Name = name;
        Phone = phone;
    }
}