namespace BasakSehirBurada.Application.Features.UserRoles.Queries.GetByUserId;

public class GetByUserIdUserRoleResponse
{
    public string UserName { get; set; }
    public string Email { get; set; }

    public string City { get; set; }

    public IList<string> Roles { get; set; }
}