using BasakSehirBurada.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BasakSehirBurada.Application.Features.Authentication.Login.Commands;

public class LoginCommand : IRequest<string>
{

    public string Email { get; set; }

    public string Password { get; set; }



    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {

        private readonly UserManager<User> _userManager;

        public LoginCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var emailUser = await _userManager.FindByEmailAsync(request.Email);

            if(emailUser is null)
            {
                throw new NotFoundException("İlgili email e göre kullanıcı bulunamadı.");
            }



            var passwordCheck = await _userManager.CheckPasswordAsync(emailUser,request.Password);
            if(passwordCheck is false)
            {
                throw new BusinessException("Parolanız yanlış.");
            }


            return "Giriş Başarıllıdır.";
        
        }
    }

}
