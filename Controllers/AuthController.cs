using System.Security.Claims;
using dotnet.Models.DTO.AuthDTOs;
using dotnet.Repoistry;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly ITokenRepo tokenRepo;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepo tokenRepo)
    {
        this.userManager = userManager;
        this.tokenRepo = tokenRepo;
    }
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterReqDTO registerReqDTO)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerReqDTO.Username,
            Email = registerReqDTO.Username,

        };
       var identityResult =  await userManager.CreateAsync(identityUser, registerReqDTO.Password);
       if (identityResult.Succeeded)
       {
           //add roles to this user
           if(registerReqDTO.Roles != null)
               identityResult= await userManager.AddToRoleAsync(identityUser, registerReqDTO.Roles[0]);
           if (identityResult.Succeeded)
           {
               return Ok("User created a new account with password.");
           }
       }
       return BadRequest(identityResult.Errors);
    }
    
    [HttpPost]
    [Route("login")]
    //post api/auth/logic
    public async Task<IActionResult> Login([FromBody] LoginReqDTO loginReqDTO)
    {
       var user = await userManager.FindByEmailAsync(loginReqDTO.Username);
       if (user != null)
       {
         var res=  await userManager.CheckPasswordAsync(user, loginReqDTO.Password);
         if (res)
         {
             //get roles for this user
             var roles = await userManager.GetRolesAsync(user);
             //create token
           var jwtToken =  tokenRepo.CreateJWTToken(user, roles.ToList());
             return Ok(new {
                 token = jwtToken,
                 message= "Login successful"
             });
         }
       }
       return BadRequest("username or password is incorrect.");
    }
}