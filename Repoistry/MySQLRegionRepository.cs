using dotnet.Data;
using dotnet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Repoistry;

public class MySQLRegionRepository : IRegionRepositry

{
    private readonly WalkDbContext dbContext;

    public MySQLRegionRepository(WalkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Region>> GetAllRegionsAsync()
    {
        var regions = await dbContext.Regions.ToListAsync();
        return regions;
    }

    public async Task<Region?> GetRegionByIdAsync(Guid regionId)
    {
        return dbContext.Regions.Find(regionId);
    }

    public async Task<Region> CreateRegionAsync(Region region)
    {
         await dbContext.Regions.AddAsync(region);
         await dbContext.SaveChangesAsync();
         return region;
    }

    public async Task<Region?> UpdateRegionAsync(Guid id ,Region region)
    {
        var existingRegion = await dbContext.Regions.FindAsync(id);
        if(existingRegion == null) return null;
        existingRegion.Name = region.Name;
        existingRegion.Code = region.Code;
        existingRegion.RegionImageURL = region.RegionImageURL;
        await dbContext.SaveChangesAsync();
        return existingRegion;
    }

    public async Task<Region?> DeleteRegionAsync(Guid regionId)
    {
        var existingRegion = dbContext.Regions.Find(regionId);
        if(existingRegion == null) return null;
        
         dbContext.Regions.Remove(existingRegion);
         await dbContext.SaveChangesAsync();
         return existingRegion;


    }
}