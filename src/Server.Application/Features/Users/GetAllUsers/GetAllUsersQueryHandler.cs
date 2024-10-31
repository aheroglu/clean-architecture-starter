using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Application.Common;
using Server.Application.Services;
using Server.Domain.Entities;

namespace Server.Application.Features.Users.GetAllUsers;

public sealed class GetAllUsersQueryHandler(
    UserManager<AppUser> userManager,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryResponse>>>
{
    private static string key = "users";
    private static TimeSpan expiration = TimeSpan.FromMinutes(10);

    public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var cachedUsers = cacheService
            .Get<List<GetAllUsersQueryResponse>>(key);

        if (cachedUsers is not null) return Result<List<GetAllUsersQueryResponse>>
                .Success(cachedUsers);

        var users = mapper
            .Map<List<GetAllUsersQueryResponse>>(await userManager
            .Users
            .ToListAsync(cancellationToken));

        cacheService
            .Set(key, users, expiration);

        return new(
            null,
            null,
            users.Adapt<List<GetAllUsersQueryResponse>>());
    }
}
