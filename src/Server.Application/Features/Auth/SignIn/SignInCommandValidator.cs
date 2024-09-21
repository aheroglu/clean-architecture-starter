using FluentValidation;

namespace Server.Application.Features.Auth.SignIn;

public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(p => p.UserNameOrEmail).NotEmpty().NotNull();
        RuleFor(p => p.Password).NotEmpty().NotNull();
    }
}
