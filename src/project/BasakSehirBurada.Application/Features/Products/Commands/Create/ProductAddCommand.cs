using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Commands.Create;

public class ProductAddCommand  : IRequest<string>
{
    public string Name { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }


    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, string>
    {
        public Task<string> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            string name = "asd";
            name.StartsWithA();

            return Task.FromResult($"Ürün Adı : {request.Name}");
        }
    }

}
