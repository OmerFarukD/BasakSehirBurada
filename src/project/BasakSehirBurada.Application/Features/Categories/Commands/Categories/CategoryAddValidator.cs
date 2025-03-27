using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasakSehirBurada.Application.Features.Categories.Commands.Categories
{
   public class CategoryAddValidator : AbstractValidator<CategoryAddCommand>
    {


        public CategoryAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adı Boş Olamaz")
                .MinimumLength(2).WithMessage("Kategori Adı minium 2 karakterli olmalıdır.");
        }
    }
}
