using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Application.Common;
using Server.Domain.Entities;

namespace Server.Application.Features.Auth.SignUp;

public sealed class SignUpCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<SignUpCommand, Result<SignUpCommandResponse>>
{
    public async Task<Result<SignUpCommandResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        bool isUserNameExists = await userManager
            .Users
            .AnyAsync(p => p.UserName == request.UserName, cancellationToken);

        if (isUserNameExists) return new(null, new List<string> { "User name already exists!" }, null);

        bool isEmailExists = await userManager
            .Users
            .AnyAsync(p => p.Email == request.Email, cancellationToken);

        if (isEmailExists) return new(null, new List<string> { "Email already exists!" }, null);

        AppUser user = new()
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await userManager
            .CreateAsync(user, request.Password);

        if (result.Errors.Any()) return new(
            null,
            result.Errors.Select(p => p.Description).ToList(),
            null);

        return new(
            "User was successfully created",
            null,
            user.Adapt<SignUpCommandResponse>());
    }
}
