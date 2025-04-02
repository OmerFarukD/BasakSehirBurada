using AutoMapper;
using BasakSehirBurada.Application.Features.Products.Constants;
using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetListNameContains;

public class GetListProductNameContainsQuery : IRequest<List<GetListProductNameResponseDto>>, ICachableRequest
{
    public string Text { get; set; }

    public string? CacheKey => $"GetListProductNameContains({Text})";

    public bool ByPassCache => false;

    public string? CacheGroupKey => ProductConstants.ProductsCacheGroup;

    public TimeSpan? SlidingExpiration => null;

    public class GetListProductNameContainsQueryHandler : IRequestHandler<GetListProductNameContainsQuery, List<GetListProductNameResponseDto>>
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetListProductNameContainsQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<GetListProductNameResponseDto>> Handle(GetListProductNameContainsQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _productRepository
                .GetAllAsync(filter: x=>x.Name.Contains(request.Text), enableTracking:false, cancellationToken:cancellationToken);



            var response = _mapper.Map<List<GetListProductNameResponseDto>>(products);

            return response;
        }
    }

}
