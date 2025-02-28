using BasakSehirBurada.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BasakSehirBurada.Application.Features.Authentication.Register;

public class RegisterCommand : IRequest<string>
{

    public string UserName { get; set; }

    public string Email { get; set; }

    public string City { get; set; }

    public string Password { get; set; }





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


            IdentityResult result = await _userManager.CreateAsync(user,request.Password);

            if (result.Succeeded)
            {
                return "Kayıt başarılı.";
            }


            return "Hata var";
        }
    }

}
