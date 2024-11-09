using dotnet.Models.DTO.DifficultyDTOs;
using dotnet.Models.DTO.RegionDTOs;

namespace dotnet.Models.DTO.WalksDTOs;

public class WalkDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; }
    
    //DTO navigation propriety
    public DifficultyDTO Difficulty { get; set; }
    public RegionDTO Region { get; set; }
}
