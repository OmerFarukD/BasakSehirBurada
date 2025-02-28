using AutoMapper;
using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using MediatR;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetlistPriceRange;

public class GetListProductPriceRangeQuery : IRequest<List<GetListProductPriceRangeResponseDto>>
{

    public double Min { get; set; }

    public double Max { get; set; }



    public class GetListProductPriceRangeQueryHandler : IRequestHandler<GetListProductPriceRangeQuery, List<GetListProductPriceRangeResponseDto>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListProductPriceRangeQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListProductPriceRangeResponseDto>> Handle(GetListProductPriceRangeQuery request, CancellationToken cancellationToken)
        {
            double min = request.Min;
            double max = request.Max;

            List<Product> products = await _productRepository
                .GetAllAsync(filter: x=>x.Price<=max && x.Price>=min , enableTracking:false, cancellationToken:cancellationToken);


            var response = _mapper.Map<List<GetListProductPriceRangeResponseDto>>(products);


            return response;


        }
    }

}
