namespace BasakSehirBurada.Application.Features.Products.Queries.GetById;

public class GetByIdProductResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    public string CategoryName { get; set; }
}
