using BasakSehirBurada.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BasakSehirBurada.Application.Features.UserRoles.Commands.Create;

public class AddUserRoleCommand : IRequest<string>
{

    public string UserId { get; set; }

    public string RoleId { get; set; }


    public class AddUserRoleCommandHandler : IRequestHandler<AddUserRoleCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddUserRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId);

            if(user is null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            IdentityRole? role = await _roleManager.FindByIdAsync(request.RoleId);

            if(role is null)
            {
                throw new NotFoundException("Rol bulunamadı.");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var roleIsExist = userRoles.Any(x=> x==role.Name);

            if (roleIsExist)
            {
                throw new BusinessException("Kullanıcının bu rolü zaten mevcut.");
            }


            IdentityResult addRoleResult = await _userManager
                .AddToRoleAsync(user,role.Name);

            if (!addRoleResult.Succeeded)
            {
                var errors = addRoleResult.Errors.Select(x => x.Description).ToList();

                throw new AuthorizationException(errors);
            }

            return "Kullanıcı ya rol eklendi.";


        }
    }


}
