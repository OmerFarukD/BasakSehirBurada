using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using Core.Application.Pipelines.Performance;
using MediatR;

namespace BasakSehirBurada.Application.Features.Categories.Queries.GetList;

public class GetListCategoryQuery : IRequest<List<Category>> ,IPerformanceRequest
{
    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, List<Category>>
    {

        private readonly ICategoryRepository _categoryRepository;

        public GetListCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            List<Category> categories = await _categoryRepository.GetAllAsync(cancellationToken:cancellationToken);
            return categories;
        }
    }
}
