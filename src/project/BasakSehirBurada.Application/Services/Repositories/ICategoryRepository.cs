using BasakSehirBurada.Domain.Entities;
using Core.Persistence.Repositories;

namespace BasakSehirBurada.Application.Services.Repositories;

public interface ICategoryRepository : IAsyncRepository<Category,int>, IRepository<Category,int>
{
    
}