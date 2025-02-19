

using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasakSehirBurada.Application.Features.Categories.Queries.GetList;

public class GetListCategoryQuery : IRequest<List<Category>>
{


    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, List<Category>>
    {
        private readonly BaseDbContext _context;

        public GetListCategoryQueryHandler(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            List<Category> categories = await _context.Categories.ToListAsync();

            return categories;

        }

    }
}
