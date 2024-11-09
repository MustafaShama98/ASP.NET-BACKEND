using System.ComponentModel.DataAnnotations;

namespace dotnet.Models.DTO.AuthDTOs;

public class RegisterReqDTO
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }
    
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public string[] Roles { get; set; }  
}