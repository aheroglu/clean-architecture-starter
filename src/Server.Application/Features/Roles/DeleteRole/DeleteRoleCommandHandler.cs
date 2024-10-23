using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Server.Application.Common;
using Server.Domain.Entities;

namespace Server.Application.Features.Roles.DeleteRole;

public sealed class DeleteRoleCommandHandler(
    RoleManager<AppRole> roleManager) : IRequestHandler<DeleteRoleCommand, Result<DeleteRoleCommandResponse>>
{
    public async Task<Result<DeleteRoleCommandResponse>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        AppRole? role = await roleManager
            .FindByIdAsync(request.Id);

        if (role is null) return Result<DeleteRoleCommandResponse>.Failure("Role not found!");

        var result = await roleManager
            .DeleteAsync(role);

        if (result.Errors.Any()) return Result<DeleteRoleCommandResponse>.Failure(result.Errors.Select(p => p.Description).ToList());

        return Result<DeleteRoleCommandResponse>.Success(
            "Role was successfully deleted",
            role.Adapt<DeleteRoleCommandResponse>());
    }
}
