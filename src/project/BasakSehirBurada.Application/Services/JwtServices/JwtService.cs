using BasakSehirBurada.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BasakSehirBurada.Application.Services.JwtServices;

public class JwtService : IJwtService
{

    private readonly UserManager<User> _userManager;
    private readonly CustomTokenOptions _customTokenOptions;

    public JwtService(UserManager<User> userManager, IOptions<CustomTokenOptions> options)
    {
        _userManager = userManager;
        _customTokenOptions = options.Value;
    }

    public async Task<AccessTokenDto> CreateTokenAsync(User user)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOptions.AccessTokenExpiration);
        var symetricKey = GetSecurityKey(_customTokenOptions.SecurityKey);


        SigningCredentials signingCredentials = new SigningCredentials(symetricKey,SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: _customTokenOptions.Issuer,
            expires: accessTokenExpiration,
            signingCredentials: signingCredentials,
            claims: await GetClaims(user)
            );


        var jwtHandler = new JwtSecurityTokenHandler();


        string token = jwtHandler.WriteToken(jwt);


        AccessTokenDto accessTokenDto = new AccessTokenDto
        {
            Token = token,
            TokenExpiration = accessTokenExpiration
        };


        return accessTokenDto;
    }



    private async Task<List<Claim>> GetClaims(User user)
    {
        var claimList = new List<Claim>()
        {

            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("Email",user.Email),
        };

        var roles = await _userManager.GetRolesAsync(user);

        if (roles.Count > 0)
        {
            claimList.AddRange(roles.Select(x => new Claim(ClaimTypes.Role,x)));
        }


        return claimList;
    }

    private SecurityKey GetSecurityKey(string key)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}
