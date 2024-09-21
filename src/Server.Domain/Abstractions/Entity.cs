namespace Server.Domain.Abstractions;
public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
