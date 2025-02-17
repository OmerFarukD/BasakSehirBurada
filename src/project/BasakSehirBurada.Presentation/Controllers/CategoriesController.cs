using BasakSehirBurada.Application.Features.Categories.Commands.Categories;
using BasakSehirBurada.Application.Features.Categories.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasakSehirBurada.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddCommand command)
        {
            var result = await mediator.Send(command);

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
