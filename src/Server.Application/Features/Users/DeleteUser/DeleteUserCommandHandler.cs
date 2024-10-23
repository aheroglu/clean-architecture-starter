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

        if (user is null) return Result<DeleteUserCommandResponse>.Failure("User not found!");

        var result = await userManager
             .DeleteAsync(user);

        if (result.Errors.Any()) return Result<DeleteUserCommandResponse>.Failure(result.Errors.Select(p => p.Description).ToList());

        return Result<DeleteUserCommandResponse>.Success(
            "User was successfully deleted",
            user.Adapt<DeleteUserCommandResponse>());
    }
}
