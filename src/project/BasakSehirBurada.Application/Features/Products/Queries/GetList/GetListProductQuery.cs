using AutoMapper;
using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetList;

public class GetListProductQuery : IRequest<List<GetListProductResponseDto>>
{
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
