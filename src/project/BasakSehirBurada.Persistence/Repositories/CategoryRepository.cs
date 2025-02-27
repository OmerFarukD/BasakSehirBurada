using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence.Contexts;
using Core.Persistence.Repositories;

namespace BasakSehirBurada.Persistence.Repositories;

public sealed class CategoryRepository : EfRepositoryBase<Category,int,BaseDbContext>, ICategoryRepository
{
    public CategoryRepository(BaseDbContext context) : base(context)
    {
    }
}