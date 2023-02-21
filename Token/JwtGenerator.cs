using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NetKubernetes.Models;
using Microsoft.IdentityModel.Tokens;

namespace NetKubernetes.Token;

public class JwtGenerator : IJwtGenerator
{
    public string CreateToken(UserApp user)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
            new Claim("userId", user.Id),
            new Claim("email", user.Email!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
        var credencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescription = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(30),
            SigningCredentials = credencials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }
}