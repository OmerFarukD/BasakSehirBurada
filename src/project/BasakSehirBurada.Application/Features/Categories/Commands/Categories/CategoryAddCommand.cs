﻿using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace BasakSehirBurada.Application.Features.Categories.Commands.Categories;

public class CategoryAddCommand : IRequest<Category>, IRoleExists
{
    public string Name { get; set; }

    public string[] Roles => ["User"];
}


public class CategoryAddCommandHandler : IRequestHandler<CategoryAddCommand, Category>
{

    private readonly ICategoryRepository _categoryRepository;

    public CategoryAddCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
    {
        Category category = new Category
        {
            Name = request.Name
        };
        await _categoryRepository.AddAsync(category, cancellationToken);
        return category;
    }
}
