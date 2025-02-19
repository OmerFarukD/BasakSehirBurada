using AutoMapper;
using BasakSehirBurada.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetDetails
{
   public  class GetDetailsProductQuery : IRequest<List<GetDetailsProductResponseDto>>
    {



        public class GetDetailsProductQueryHandler : IRequestHandler<GetDetailsProductQuery, List<GetDetailsProductResponseDto>>
        {
            private readonly IMapper _mapper;
            private readonly BaseDbContext _context;

            public GetDetailsProductQueryHandler(IMapper mapper, BaseDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<GetDetailsProductResponseDto>> Handle(GetDetailsProductQuery request, CancellationToken cancellationToken)
            {
                var products = await _context.Products.Include(x=>x.Category).ToListAsync();

                var response = _mapper.Map<List<GetDetailsProductResponseDto>>(products);

                return response;
            }
        }
    }
}
