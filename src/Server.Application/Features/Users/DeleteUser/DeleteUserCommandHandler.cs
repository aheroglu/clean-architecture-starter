using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Server.Application.Common;
using Server.Domain.Entities;

namespace Server.Application.Features.Users.DeleteUser;

public sealed class DeleteUserCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<DeleteUserCommand, Result<DeleteUserCommandResponse>>
{
    public async Task<Result<DeleteUserCommandResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager
            .FindByIdAsync(request.Id);

        if (user is null) return new(null, new List<string> { "User not found!" }, null);

        var result = await userManager
             .DeleteAsync(user);

        if (result.Errors.Any()) return new(
          null,
          result.Errors.Select(p => p.Description).ToList(),
          null);

        return new(
            "User was successfully deleted",
            null,
            user.Adapt<DeleteUserCommandResponse>());
    }
}
