using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence.Contexts;
using MediatR;

namespace BasakSehirBurada.Application.Features.Categories.Commands.Categories;

public class CategoryAddCommand : IRequest<Category>
{
    public string Name { get; set; }
}


public class CategoryAddCommandHandler : IRequestHandler<CategoryAddCommand, Category>
{

    private readonly BaseDbContext _context;

    public CategoryAddCommandHandler(BaseDbContext context)
    {
        _context = context;
    }

    public async Task<Category> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
    {
        Category category = new Category
        {
            Name = request.Name
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }
}
