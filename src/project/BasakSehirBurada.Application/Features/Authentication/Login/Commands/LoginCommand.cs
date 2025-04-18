﻿using BasakSehirBurada.Application.Services.JwtServices;
using BasakSehirBurada.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BasakSehirBurada.Application.Features.Authentication.Login.Commands;

public class LoginCommand : IRequest<AccessTokenDto>
{

    public string Email { get; set; }

    public string Password { get; set; }



    public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessTokenDto>
    {

        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AccessTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
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


            AccessTokenDto token = await _jwtService.CreateTokenAsync(emailUser);


            return token;
        
        }
    }

}
