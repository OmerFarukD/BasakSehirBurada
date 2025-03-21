using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BasakSehirBurada.Application.Features.Roles.Commands.Create;

public class RoleAddCommand : IRequest<string>
{

    public string Name { get; set; }



    public class RoleAddCommandHandler : IRequestHandler<RoleAddCommand, string>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleAddCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> Handle(RoleAddCommand request, CancellationToken cancellationToken)
        {
            // İlgili Rol veri tabanında var mı yok mu ?

            bool roleIsPresent = await _roleManager.RoleExistsAsync(request.Name);

            if (roleIsPresent)
            {
                throw new BusinessException("Eklemek istediğiniz rol adı benzersiz olmalıdır.");
            }

            IdentityRole role = new IdentityRole()
            {
                Name = request.Name
            };

            await _roleManager.CreateAsync(role);

            return "Rol Başarıyla Eklendi.";

        }
    }

}
