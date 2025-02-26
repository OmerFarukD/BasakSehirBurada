using AutoMapper;
using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence.Contexts;
using MediatR;
namespace BasakSehirBurada.Application.Features.Products.Queries.GetById;
public class GetByIdProductQuery  : IRequest<GetByIdProductResponseDto>
{
    public int Id { get; set; }

    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductResponseDto>
    {
        private readonly BaseDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdProductQueryHandler(BaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<GetByIdProductResponseDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            Product product = await _context.Products.FindAsync(request.Id);

            var response = _mapper.Map<GetByIdProductResponseDto>(product);

            return response;
        }
    }

}
