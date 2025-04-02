using AutoMapper;
using BasakSehirBurada.Application.Features.Products.Constants;
using BasakSehirBurada.Application.Services.RedisServices;
using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetList;

public class GetListProductQuery : IRequest<List<GetListProductResponseDto>>, ICachableRequest
{

    public string? CacheKey => $"GetListProduct";

    public bool ByPassCache => false;

    public string? CacheGroupKey => ProductConstants.ProductsCacheGroup;

    public TimeSpan? SlidingExpiration => null;

    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, List<GetListProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetListProductQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<GetListProductResponseDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {

            List<Product> products = await _productRepository.GetAllAsync(enableTracking:false,cancellationToken:cancellationToken);

            var responses = _mapper.Map<List<GetListProductResponseDto>>(products);

      
            return responses;

        }
    }
}
