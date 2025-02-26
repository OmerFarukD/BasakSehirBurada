
using BasakSehirBurada.Domain.Entities;
using Core.Persistence.Repositories;

namespace BasakSehirBurada.Application.Services.Repositories;

public interface IProductRepository : IAsyncRepository<Product,int>, IRepository<Product,int>
{
}
