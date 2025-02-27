using AutoMapper;
using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using MediatR;
namespace BasakSehirBurada.Application.Features.Products.Queries.GetById;
public class GetByIdProductQuery  : IRequest<GetByIdProductResponseDto>
{
    public int Id { get; set; }

    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<GetByIdProductResponseDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetAsync(filter: x=> x.Id == request.Id,
                enableTracking:false,cancellationToken:cancellationToken);

            var response = _mapper.Map<GetByIdProductResponseDto>(product);

            return response;
        }
    }

}
