using AutoMapper;
using BasakSehirBurada.Application.Features.Products.Commands.Create;
using BasakSehirBurada.Application.Features.Products.Queries.GetById;
using BasakSehirBurada.Application.Features.Products.Queries.GetDetails;
using BasakSehirBurada.Application.Features.Products.Queries.GetList;
using BasakSehirBurada.Application.Features.Products.Queries.GetListNameContains;
using BasakSehirBurada.Application.Features.Products.Queries.GetlistPriceRange;
using BasakSehirBurada.Domain.Entities;

namespace BasakSehirBurada.Application.Features.Products.Profiles
{
   public class ProductsMapper : Profile
    {

        public ProductsMapper()
        {
            CreateMap<ProductAddCommand , Product>();
            CreateMap<Product,GetListProductResponseDto>();
            CreateMap<Product,GetDetailsProductResponseDto>();
            CreateMap<Product,GetByIdProductResponseDto>();
            CreateMap<Product, GetListProductPriceRangeResponseDto>();
            CreateMap<Product, GetListProductNameResponseDto>();
        }
    }
}
