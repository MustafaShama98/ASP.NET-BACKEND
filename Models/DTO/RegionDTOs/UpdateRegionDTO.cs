namespace dotnet.Models.DTO.RegionDTOs;

public class UpdateRegionDTO
{
    public string Code { get;  set; }
    public string Name { get;  set; }
    public string? RegionImageURL { get;  set; } //can have null values
}