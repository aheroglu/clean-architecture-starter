using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.Users.DeleteUser;
using Server.Application.Features.Users.GetAllUsers;
using Server.Presentation.Abstractions;

namespace Server.Presentation.Controllers;

public sealed class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllUsersQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return Ok(response);
    }
}
