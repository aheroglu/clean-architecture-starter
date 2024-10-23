using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Application.Common;
using Server.Domain.Entities;

namespace Server.Application.Features.Roles.CreateRole;

public sealed class CreateRoleCommandHandler(
    RoleManager<AppRole> roleManager) : IRequestHandler<CreateRoleCommand, Result<CreateRoleCommandResponse>>
{
    public async Task<Result<CreateRoleCommandResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        bool isRoleExists = await roleManager
            .Roles
            .AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isRoleExists) return Result<CreateRoleCommandResponse>.Failure("Role already exists!");

        AppRole role = new()
        {
            Name = request.Name
        };

        var result = await roleManager
             .CreateAsync(role);

        if (result.Errors.Any()) return Result<CreateRoleCommandResponse>.Failure(result.Errors.Select(p => p.Description).ToList());

        return Result<CreateRoleCommandResponse>.Success(
            "Role was successfully created",
            role.Adapt<CreateRoleCommandResponse>());
    }
}
