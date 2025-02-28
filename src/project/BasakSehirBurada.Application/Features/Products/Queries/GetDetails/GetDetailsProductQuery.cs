using AutoMapper;
using BasakSehirBurada.Application.Services.Repositories;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetDetails
{
   public  class GetDetailsProductQuery : IRequest<List<GetDetailsProductResponseDto>>
    {
        public class GetDetailsProductQueryHandler : IRequestHandler<GetDetailsProductQuery, List<GetDetailsProductResponseDto>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _repository;

            public GetDetailsProductQueryHandler(IMapper mapper, IProductRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<List<GetDetailsProductResponseDto>> Handle(GetDetailsProductQuery request, CancellationToken cancellationToken)
            {
                var products = await _repository.GetAllAsync(enableTracking:false,cancellationToken:cancellationToken);

                var response = _mapper.Map<List<GetDetailsProductResponseDto>>(products);

                return response;
            }
        }
    }
}
