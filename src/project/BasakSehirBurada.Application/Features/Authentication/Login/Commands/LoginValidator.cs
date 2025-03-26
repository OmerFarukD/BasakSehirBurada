using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasakSehirBurada.Application.Features.Authentication.Login.Commands
{
   public class LoginValidator : AbstractValidator<LoginCommand>
    {

        // Email : "Email Boş Olamaz",
        // Password: "Parola Alanı boş olamaz."

        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş olamaz.")
              
                .Must(x => EmailFormat(x)).WithMessage("Email Formatında Değil");


            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola alanı Boş olamaz")
                .MinimumLength(6).WithMessage("Parola alanı minimum 6 haneli olmalıdır.");
        }



        private bool EmailFormat(string email)
        {
            return email.EndsWith("@gmail.com");
          
        }
    }
}
