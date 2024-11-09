using dotnet.Models.Domain;

namespace dotnet.Repoistry;

public interface IRegionRepositry
{
   public Task<List<Region>> GetAllRegionsAsync();
   
   Task<Region?> GetRegionByIdAsync(Guid regionId);
   
   Task<Region> CreateRegionAsync(Region region);
   
   Task<Region?> UpdateRegionAsync( Guid id , Region region);
   
   Task<Region?> DeleteRegionAsync(Guid regionId);
}