using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.Roles.CreateRole;
using Server.Presentation.Abstractions;

namespace Server.Presentation.Controllers;

public sealed class RolesController : ApiController
{
    public RolesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
