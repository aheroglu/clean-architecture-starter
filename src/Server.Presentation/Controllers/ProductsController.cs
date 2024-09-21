using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.Products.CreateProduct;
using Server.Application.Features.Products.DeleteProductById;
using Server.Application.Features.Products.GetAllProducts;
using Server.Application.Features.Products.GetProductById;
using Server.Application.Features.Products.UpdateProduct;
using Server.Presentation.Abstractions;

namespace Server.Presentation.Controllers;

public sealed class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteProductByIdCommand(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
