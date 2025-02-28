using BasakSehirBurada.Application.Features.Products.Commands.Create;
using BasakSehirBurada.Application.Features.Products.Queries.GetById;
using BasakSehirBurada.Application.Features.Products.Queries.GetDetails;
using BasakSehirBurada.Application.Features.Products.Queries.GetList;
using BasakSehirBurada.Application.Features.Products.Queries.GetListNameContains;
using BasakSehirBurada.Application.Features.Products.Queries.GetlistPriceRange;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BasakSehirBurada.Presentation.Controllers;

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


    [HttpGet("details")]
    public async Task<IActionResult> GetAllDetails()
    {
        var result = await mediator.Send(new GetDetailsProductQuery());
        return Ok(result);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetByIdProductQuery() { Id = id});
        return Ok(result);
    }


    [HttpGet("getallpricerange")]
    public async Task<IActionResult> GetAllByPriceRange(double min, double max)
    {
        GetListProductPriceRangeQuery query = new GetListProductPriceRangeQuery()
        {
            Min = min,
            Max = max
        };

        var result = await mediator.Send(query);

        return Ok(result);
    }


    [HttpGet("getallbynamecontains")]
    public async Task<IActionResult> GetAllByNameContains(string text)
    {
        GetListProductNameContainsQuery query = new() { Text = text };

        var response = await mediator.Send(query);

        return Ok(response);
    }

}
