using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Roles.CreateRole;

public sealed record CreateRoleCommand(
    string Name) : IRequest<Result<CreateRoleCommandResponse>>;
