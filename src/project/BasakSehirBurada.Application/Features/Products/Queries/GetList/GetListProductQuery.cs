using AutoMapper;
using BasakSehirBurada.Application.Services.RedisServices;
using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetList;

public class GetListProductQuery : IRequest<List<GetListProductResponseDto>>
{

    public int Index { get; set; }
    public int Size { get; set; }
    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, List<GetListProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IRedisService _redisService;

        public GetListProductQueryHandler(IMapper mapper, IProductRepository productRepository, IRedisService redisService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _redisService = redisService;
        }

        public async Task<List<GetListProductResponseDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            var cachedData = await _redisService.GetDataAsync<List<GetListProductResponseDto>>("products");
            if (cachedData != null)
            {
                return cachedData;
            }



            List<Product> products = await _productRepository.GetAllAsync(enableTracking:false,cancellationToken:cancellationToken);

            var responses = _mapper.Map<List<GetListProductResponseDto>>(products);

            await _redisService.AddDataAsync($"products({request.Index}, {request.Size})",responses);

            return responses;

        }
    }
}
