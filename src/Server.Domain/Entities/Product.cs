using Server.Domain.Abstractions;

namespace Server.Domain.Entities;

public sealed class Product : Entity
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
