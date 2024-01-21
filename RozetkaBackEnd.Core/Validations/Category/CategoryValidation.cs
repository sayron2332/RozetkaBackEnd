using FluentValidation;
using RozetkaBackEnd.Core.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Validations.Category
{
    public class CategoryValidation : AbstractValidator<CategoryDto>
    {
        public CategoryValidation()
        {
             RuleFor(x => x.Name).NotEmpty().MinimumLength(4).WithMessage("Minimum length 6");
        }
    }
}
