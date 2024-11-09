using dotnet.Data;
using dotnet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace dotnet.Repoistry;

public class MySQLWalkRepository : IWalkRepoitry
{
    private readonly WalkDbContext dbContext;

    public MySQLWalkRepository(WalkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Walk> CreateWalk(Walk walk)
    {
       await dbContext.Walks.AddAsync(walk);
       await dbContext.SaveChangesAsync();
       return walk;
    }

    public async Task<Walk?> GetWalkById(Guid id)
    {
        return await dbContext.Walks.FindAsync(id);
    }

    public async Task<List<Walk>> GetAllWalks()
    {
        // return await dbContext.Walks.ToListAsync();
        return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }

    public  async Task<Walk> UpdateWalkById(Guid id ,Walk walk)
    {
        
        var existingWalk = await dbContext.Walks.FindAsync(id);
        if (existingWalk == null) return null;
        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.DifficultyId = walk.DifficultyId;
        existingWalk.RegionId = walk.RegionId;
      await dbContext.SaveChangesAsync();
      return existingWalk;
    }

    public async Task<Walk?> DeleteWalk(Guid id)
    {
        var existingWalk = await dbContext.Walks.FindAsync(id);
        if (existingWalk == null) return null;
        dbContext.Walks.Remove(existingWalk);
        await dbContext.SaveChangesAsync();
        return existingWalk;
        
    }
}