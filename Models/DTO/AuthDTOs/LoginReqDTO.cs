using System.ComponentModel.DataAnnotations;

namespace dotnet.Models.DTO.AuthDTOs;

public class LoginReqDTO
{
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}