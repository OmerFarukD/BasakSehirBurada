using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasakSehirBurada.Application.Features.Roles.Queries.GetList;

public class GetListRoleQuery : IRequest<List<IdentityRole>>
{
    public class GetListRoleQueryHandler : IRequestHandler<GetListRoleQuery, List<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetListRoleQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<IdentityRole>> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles;
        }
    }
}
