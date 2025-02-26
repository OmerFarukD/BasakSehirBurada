using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence.Contexts;
using Core.Persistence.Repositories;

namespace BasakSehirBurada.Persistence.Repositories;

public class ProductRepository : EfRepositoryBase<Product, int, BaseDbContext>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }
}
