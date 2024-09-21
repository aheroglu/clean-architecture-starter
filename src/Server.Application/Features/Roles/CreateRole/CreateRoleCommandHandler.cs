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

        if (isRoleExists) return new(
            null,
            new List<string> { "Role already exists!" },
            null);

        AppRole role = new()
        {
            Name = request.Name
        };

        var result = await roleManager
             .CreateAsync(role);

        if (result.Errors.Any()) return new(null,
            result.Errors.Select(p => p.Description).ToList(),
            null);

        return new(
            "Role was successfully created",
            null,
            role.Adapt<CreateRoleCommandResponse>());
    }
}
