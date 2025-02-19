using AutoMapper;
using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetList;

public class GetListProductQuery : IRequest<List<GetListProductResponseDto>>
{
    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, List<GetListProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly BaseDbContext _context;

        public GetListProductQueryHandler(IMapper mapper, BaseDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetListProductResponseDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _context.Products.ToListAsync();

            var responses = _mapper.Map<List<GetListProductResponseDto>>(products);

            return responses;
        }
    }
}
