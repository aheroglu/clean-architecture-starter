using FluentValidation;

namespace Server.Application.Features.Products.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().NotNull().MinimumLength(2);
        RuleFor(p => p.Price).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(p => p.Stock).NotNull().NotEmpty().GreaterThan(0);
    }
}
