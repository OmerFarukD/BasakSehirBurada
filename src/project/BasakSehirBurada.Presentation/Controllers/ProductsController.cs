using BasakSehirBurada.Application.Features.Products.Commands.Create;
using BasakSehirBurada.Application.Features.Products.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BasakSehirBurada.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddCommand command)
        {
            string result = await mediator.Send(command);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetListProductQuery query = new GetListProductQuery();

            var result = await mediator.Send(query);

            return Ok(result);
        }

    }
}
