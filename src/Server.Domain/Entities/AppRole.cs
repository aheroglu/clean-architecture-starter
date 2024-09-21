using Microsoft.AspNetCore.Identity;

namespace Server.Domain.Entities;

public sealed class AppRole : IdentityRole
{
    public AppRole()
    {
        Id = Guid.NewGuid().ToString();
    }
}
