using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Utilities.TokenHandlers;

public class TokenHandler(IConfiguration configuration) : ITokenHandler
{
  public string GenerateJwt(User user, int option)
  {
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApiSettings:SecretKey"]!));
    var credentias = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


    // create user information for token
    var userClaims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new Claim(ClaimTypes.Role, user.Role.ToString()),
    };

    // Crear detalle del token
    var jwtConfig = new JwtSecurityToken(
      configuration["Jwt:issuer"],
      configuration["Jwt:Audience"],
      claims: userClaims,
      expires: option switch
      {
        1 => DateTime.Now.AddMinutes(60),
        2 => DateTime.Now.AddMinutes(80),
        _ => throw new ArgumentOutOfRangeException(nameof(option), "Opción no válida.")
      },
      signingCredentials: credentias
    );

    return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
  }
}