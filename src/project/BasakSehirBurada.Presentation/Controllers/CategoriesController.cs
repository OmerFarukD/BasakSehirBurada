using BasakSehirBurada.Application.Features.Categories.Commands.Categories;
using BasakSehirBurada.Application.Features.Categories.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasakSehirBurada.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Add(CategoryAddCommand command)
        {
            var result = await mediator.Send(command);


            var claims = HttpContext.User.Claims;

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetListCategoryQuery query = new();

            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
