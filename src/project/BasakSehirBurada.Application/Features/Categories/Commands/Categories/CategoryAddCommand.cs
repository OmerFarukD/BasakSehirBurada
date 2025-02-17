using BasakSehirBurada.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasakSehirBurada.Application.Features.Categories.Commands.Categories;

public class CategoryAddCommand : IRequest<Category>
{
    public string Name { get; set; }
}


public class CategoryAddCommandHandler : IRequestHandler<CategoryAddCommand, Category>
{
    public Task<Category> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
    {

        Category category = new Category
        {
            Name = request.Name
        };
        return Task.FromResult(category);
    }
}
