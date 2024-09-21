namespace Server.Application.Features.Auth.SignUp;

public sealed record SignUpCommandResponse(
    string Id,
    string UserName,
    string Email);
