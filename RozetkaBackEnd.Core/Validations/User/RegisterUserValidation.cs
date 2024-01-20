using FluentValidation;
using RozetkaBackEnd.Core.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Validations.User
{
    public class RegisterUserValidation : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidation()
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Enter valid Email").NotEmpty().WithMessage("Не може бути пусте");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Не може бути пусте").MinimumLength(6);
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Не може бути пусте").MinimumLength(3);
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Не може бути пусте").MinimumLength(3);
        }
    }
}
