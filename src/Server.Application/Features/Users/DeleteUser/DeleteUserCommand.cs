using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Users.DeleteUser;

public sealed record DeleteUserCommand(
    string Id) : IRequest<Result<DeleteUserCommandResponse>>;
