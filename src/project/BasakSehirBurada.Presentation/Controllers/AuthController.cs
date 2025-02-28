using BasakSehirBurada.Application.Features.Authentication.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasakSehirBurada.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

}
