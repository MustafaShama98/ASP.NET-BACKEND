using AutoMapper;
using dotnet.Models.Domain;
using dotnet.Models.DTO.WalksDTOs;
using dotnet.Repoistry;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace dotnet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : Controller
{
    private IWalkRepoitry walkRepoitry;
    private IMapper mapper;

    public WalksController(IWalkRepoitry walkRepoitry,IMapper mapper)
    {
        this.walkRepoitry = walkRepoitry;
        this.mapper = mapper;
    }
    
    //we accepting dto because we need providing id when creating
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
    {
        try
        {
            // Map DTO to domain model
            var walkDomain = mapper.Map<Walk>(addWalkRequestDTO);
            if (walkDomain == null)
            {
                return BadRequest("Mapping failed.");
            }

            // Create walk in repository
            await walkRepoitry.CreateWalk(walkDomain);
        
            // Map domain model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomain);
            if (walkDTO == null)
            {
                return StatusCode(500, "Mapping to DTO failed.");
            }
        
            // Return result
            return Ok(walkDTO);
        }
        catch (Exception ex)
        {
            // Log the exception (assuming a logger is configured)
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWalks()
    {
        var walks = await walkRepoitry.GetAllWalks();
        
        //map from domain to dto
        var walksDTO = mapper.Map<List<WalkDTO>>(walks);
        return Ok(walksDTO);
    }
 
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetWalkById(Guid id)
    {
       var  walkDomain= await walkRepoitry.GetWalkById(id);
       
       var walkDTO = mapper.Map<WalkDTO>(walkDomain);
       return Ok(walkDTO);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateById([FromRoute] Guid id, UpdateWalkDTO updateWalkDTO)
    {
        
        //map to domain
        var walkDomain = mapper.Map<Walk>(updateWalkDTO);
         walkDomain = await walkRepoitry.UpdateWalkById(id,walkDomain);
         
         //map to dto
        var walkDTO  = mapper.Map<WalkDTO>(walkDomain);
        return Ok(walkDTO);
    }
    
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteWalkById([FromRoute] Guid id)
    {
        var walkDomain = await walkRepoitry.DeleteWalk(id);
        //map to DTO
        
        var walkDTOP = mapper.Map<WalkDTO>(walkDomain);
        return Ok(walkDTOP);
    }

}