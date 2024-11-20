using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Application.Common;
using Server.Application.Services;
using Server.Domain.Entities;

namespace Server.Application.Features.Auth.SignUp;

public sealed class SignUpCommandHandler(
    UserManager<AppUser> userManager,
    IEmailService emailService) : IRequestHandler<SignUpCommand, Result<SignUpCommandResponse>>
{
    public async Task<Result<SignUpCommandResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        bool isUserNameExists = await userManager
            .Users
            .AnyAsync(p => p.UserName == request.UserName, cancellationToken);

        if (isUserNameExists) return Result<SignUpCommandResponse>.Failure("User name already exists!");

        bool isEmailExists = await userManager
            .Users
            .AnyAsync(p => p.Email == request.Email, cancellationToken);

        if (isEmailExists) return Result<SignUpCommandResponse>.Failure("Email already exists!");

        AppUser user = new()
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await userManager
            .CreateAsync(user, request.Password);

        if (result.Errors.Any()) return Result<SignUpCommandResponse>.Failure(result.Errors.Select(p => p.Description).ToList());

        string body = @"
        <h1>Welcome to [YOUR APP NAME HERE]!</h1>   
        <p>Your account created successfully.</p>";

        emailService
            .SendEmail(
                user.UserName,
                user.Email,
                "Your Account Created Successfully!",
                body);

        return Result<SignUpCommandResponse>.Success(
            "User was successfully created",
            user.Adapt<SignUpCommandResponse>());
    }
}
