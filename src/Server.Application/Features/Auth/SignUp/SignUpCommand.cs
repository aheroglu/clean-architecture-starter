using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Auth.SignUp;

public sealed record SignUpCommand(
    string UserName,
    string Email,
    string Password) : IRequest<Result<SignUpCommandResponse>>;