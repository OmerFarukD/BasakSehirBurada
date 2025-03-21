using BasakSehirBurada.Application.Features.Roles.Commands.Create;
using BasakSehirBurada.Application.Features.Roles.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BasakSehirBurada.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IMediator mediator) : ControllerBase
{


    [HttpPost("add")]
    public async Task<IActionResult> Add(RoleAddCommand command)
    {
        var result = await mediator.Send(command);

        return Ok(result);
    }


    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetListRoleQuery());

        return Ok(result);
    }

}
