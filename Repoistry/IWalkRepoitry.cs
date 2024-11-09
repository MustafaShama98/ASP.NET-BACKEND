using dotnet.Models.Domain;
using dotnet.Models.DTO.WalksDTOs;

namespace dotnet.Repoistry;

public interface IWalkRepoitry //repositires always use domain models, because we injeting the object to db
{
    public Task<Walk> CreateWalk(Walk walk);
    public Task<Walk?> GetWalkById(Guid id);
    public Task<List<Walk>> GetAllWalks();
    public Task<Walk?> UpdateWalkById(Guid id, Walk walk);
    public Task<Walk?>  DeleteWalk(Guid id);
}