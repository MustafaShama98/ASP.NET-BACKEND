using Microsoft.AspNetCore.Identity;

namespace dotnet.Repoistry;

public interface ITokenRepo
{
    string CreateJWTToken(IdentityUser user, List<string> roles);
}