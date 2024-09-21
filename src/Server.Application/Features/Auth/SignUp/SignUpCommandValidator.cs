using FluentValidation;

namespace Server.Application.Features.Auth.SignUp;

public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(p => p.UserName).NotNull().NotEmpty();
        RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(p => p.Password).NotNull().NotEmpty();
    }
}
