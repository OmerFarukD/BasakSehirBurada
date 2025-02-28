using Microsoft.AspNetCore.Identity;

namespace BasakSehirBurada.Domain.Entities;

public class User : IdentityUser
{
    public string City { get; set; }

}
