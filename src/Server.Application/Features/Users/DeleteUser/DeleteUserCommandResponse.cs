namespace Server.Application.Features.Users.DeleteUser;

public sealed record DeleteUserCommandResponse(
    string Id,
    string UserName,
    string Email);
