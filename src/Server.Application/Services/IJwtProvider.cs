using Server.Domain.Entities;

namespace Server.Application.Services;

public interface IJwtProvider
{
    string GenerateToken(AppUser user);
}
