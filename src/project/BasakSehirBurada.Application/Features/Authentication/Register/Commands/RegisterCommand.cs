using BasakSehirBurada.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BasakSehirBurada.Application.Features.Authentication.Register.Commands;

public class RegisterCommand : IRequest<string>
{

    public string UserName { get; set; }

    public string Email { get; set; }

    public string City { get; set; }

    public string Password { get; set; }




    // Aynı email e sahip bir kullanıcı ve mı yok mu 
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {

        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User user = new User()
            {
                City = request.City,
                UserName = request.UserName,
                Email = request.Email,
            };

            var emailUserCheck = await _userManager.FindByEmailAsync(request.Email);

            if(emailUserCheck is not null)
            {
                throw new BusinessException("Kullanıcı Emaili benzersiz olmalıdır.");
            }



            IdentityResult result = await _userManager.CreateAsync(user,request.Password);

            if (result.Succeeded)
            {
                return "Kayıt başarılı.";
            }


            return "Hata var";
        }
    }

}
