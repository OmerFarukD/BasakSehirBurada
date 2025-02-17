

using BasakSehirBurada.Domain.Entities;
using MediatR;

namespace BasakSehirBurada.Application.Features.Categories.Queries.GetList;

public class GetListCategoryQuery : IRequest<List<Category>>
{


    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, List<Category>>
    {
        public Task<List<Category>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            List<Category> categories = new List<Category>() { new Category { Id = 1, Name = "Elektronik" } };

            return Task.FromResult(categories);
        }

    }
}
