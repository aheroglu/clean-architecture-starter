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

        if (user is null) return Result<SignInCommandResponse>.Failure("User not found!");

        bool IsPasswordCorrect = await userManager
            .CheckPasswordAsync(user, request.Password);

        if (!IsPasswordCorrect) return Result<SignInCommandResponse>.Failure("Incorrect password!");

        string token = jwtProvider.GenerateToken(user);

        return Result<SignInCommandResponse>.Success(
            token,
            user.Adapt<SignInCommandResponse>());
    }
}