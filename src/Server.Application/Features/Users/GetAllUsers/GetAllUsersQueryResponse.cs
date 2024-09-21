namespace Server.Application.Features.Users.GetAllUsers;

public sealed record GetAllUsersQueryResponse(
    string Id,
    string UserName,
    string Email);
