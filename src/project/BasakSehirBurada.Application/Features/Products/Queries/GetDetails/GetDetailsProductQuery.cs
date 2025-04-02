using AutoMapper;
using BasakSehirBurada.Application.Features.Products.Constants;
using BasakSehirBurada.Application.Services.Repositories;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetDetails
{
   public  class GetDetailsProductQuery : IRequest<List<GetDetailsProductResponseDto>>, ICachableRequest

    {
        public string? CacheKey => "GetProductDetails";

        public bool ByPassCache => false;

        public string? CacheGroupKey => ProductConstants.ProductsCacheGroup;

        public TimeSpan? SlidingExpiration => null;

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
