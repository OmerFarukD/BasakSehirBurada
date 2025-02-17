using BasakSehirBurada.Domain.Entities;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetList;

public class GetListProductQuery : IRequest<List<Product>>
{




    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, List<Product>>
    {
        public Task<List<Product>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = new List<Product>()
            {
                new Product{Id=1,Name="A",Price=255,Stock=15},
                new Product{Id=2,Name="B",Price=2525,Stock=15},
                new Product{Id=3,Name="C",Price=2553,Stock=15},
                new Product{Id=4,Name="D",Price=2554,Stock=10},
            };


            return Task.FromResult(products);

        }
    }
}
