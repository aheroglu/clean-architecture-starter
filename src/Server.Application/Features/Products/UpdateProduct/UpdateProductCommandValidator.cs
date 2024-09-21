using FluentValidation;

namespace Server.Application.Features.Products.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().NotNull().MinimumLength(2);
        RuleFor(p => p.Price).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(p => p.Stock).NotNull().NotEmpty().GreaterThan(0);
    }
}
