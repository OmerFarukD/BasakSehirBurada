using AutoMapper;
using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence.Contexts;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Commands.Create;

public class ProductAddCommand  : IRequest<string>
{
    public string Name { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }


    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, string>
    {

        private readonly BaseDbContext _context;
        private readonly IMapper _mapper;

        public ProductAddCommandHandler(BaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return "Ürün Eklendi.";
        }
    }

}
