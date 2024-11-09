using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace dotnet.Repoistry;

public class TokenRepository: ITokenRepo
{
    private readonly IConfiguration configuration;

    public TokenRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public string CreateJWTToken(IdentityUser user, List<string> roles)
    {
        //create claims
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Email, user.Email));

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        Console.WriteLine(configuration["JWT:Key"]);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt.Audience"],claims
            ,null,DateTime.Now.AddMinutes(15),
            signingCredentials: creds
           
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}