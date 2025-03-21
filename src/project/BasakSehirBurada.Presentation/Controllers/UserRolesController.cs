using BasakSehirBurada.Application.Features.UserRoles.Queries.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasakSehirBurada.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController(IMediator mediator) : ControllerBase
    {


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(string id)
        {
            var response = new GetByUserIdUserRoleQuery { UserId = id };

            var result = await mediator.Send(response);
            return Ok(result);
        }
    }
}
