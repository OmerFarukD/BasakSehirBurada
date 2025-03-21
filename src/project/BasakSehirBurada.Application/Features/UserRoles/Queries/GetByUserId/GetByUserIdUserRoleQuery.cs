using BasakSehirBurada.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BasakSehirBurada.Application.Features.UserRoles.Queries.GetByUserId;

public class GetByUserIdUserRoleQuery : IRequest<GetByUserIdUserRoleResponse>
{

    public string UserId { get; set; }



    public class GetByUserIdUserRoleQueryHandler : IRequestHandler<GetByUserIdUserRoleQuery, GetByUserIdUserRoleResponse>
    {

        private readonly UserManager<User> _userManager;

        public GetByUserIdUserRoleQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetByUserIdUserRoleResponse> Handle(GetByUserIdUserRoleQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }


            var roles = await _userManager.GetRolesAsync(user);

            GetByUserIdUserRoleResponse response = new GetByUserIdUserRoleResponse()
            {
                 City = user.City,
                 Email = user.Email,
                 Roles = roles,
                 UserName = user.UserName
            };

            return response;

        }
    }

}
