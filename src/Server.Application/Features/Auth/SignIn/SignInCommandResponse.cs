namespace Server.Application.Features.Auth.SignIn;

public sealed record SignInCommandResponse(
    string Id,
    string UserName,
    string Email);
