using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Application.Common;
using Server.Domain.Entities;

namespace Server.Application.Features.Users.GetAllUsers;

public sealed class GetAllUsersQueryHandler(
    UserManager<AppUser> userManager) : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryResponse>>>
{
    public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager
            .Users
            .ToListAsync(cancellationToken);

        return new(
            null,
            null,
            users.Adapt<List<GetAllUsersQueryResponse>>());
    }
}
