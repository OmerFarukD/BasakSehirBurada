using BasakSehirBurada.Domain.Entities;
namespace BasakSehirBurada.Application.Services.JwtServices;

public interface IJwtService
{
    Task<AccessTokenDto> CreateTokenAsync(User user);
}