using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Application.Common;
using Server.Application.Services;
using Server.Domain.Entities;

namespace Server.Application.Features.Auth.SignIn;

public sealed class SignInCommandHandler(
    UserManager<AppUser> userManager,
    IJwtProvider jwtProvider) : IRequestHandler<SignInCommand, Result<SignInCommandResponse>>
{
    public async Task<Result<SignInCommandResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager
            .Users
            .FirstOrDefaultAsync(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail, cancellationToken);

        if (user is null) return new(null, new List<string> { "User not found!" }, null);

        bool IsPasswordCorrect = await userManager
            .CheckPasswordAsync(user, request.Password);

        if (!IsPasswordCorrect) return new(null, new List<string> { "Incorrect password!" }, null);

        string token = jwtProvider.GenerateToken(user);

        return new(
            token,
            null,
            user.Adapt<SignInCommandResponse>());
    }
}
