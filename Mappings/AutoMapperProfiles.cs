using AutoMapper;
using dotnet.Models.Domain;
using dotnet.Models.DTO.DifficultyDTOs;
using dotnet.Models.DTO.RegionDTOs;
using dotnet.Models.DTO.WalksDTOs;

namespace dotnet.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //main domain model to DTO or reverse
        CreateMap<Region, RegionDTO>().ReverseMap();
        
        CreateMap<AddRegionDTO, Region>().ReverseMap();
        
        CreateMap<UpdateRegionDTO,Region>().ReverseMap();
        CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();
        CreateMap<Walk, WalkDTO>().ReverseMap();
        CreateMap<UpdateRegionDTO, WalkDTO>().ReverseMap();

        CreateMap<Difficulty, DifficultyDTO>().ReverseMap();


    }
}