using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Roles.DeleteRole;

public sealed record DeleteRoleCommand(
    string Id) : IRequest<Result<DeleteRoleCommandResponse>>;
