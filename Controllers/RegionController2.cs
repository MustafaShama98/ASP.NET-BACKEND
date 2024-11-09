using dotnet.Data;
using dotnet.Models.Domain;
using dotnet.Models.DTO.RegionDTOs;
using dotnet.Repoistry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController2 : ControllerBase
    {
        private readonly WalkDbContext dbContext;
        private readonly IRegionRepositry regionRepositry;

        public RegionsController2(WalkDbContext dbContext, IRegionRepositry regionRepositry)
        {
            this.dbContext = dbContext;
            this.regionRepositry = regionRepositry;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepositry.GetAllRegionsAsync();
            return Ok(regions);
        }
        
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = await regionRepositry.GetRegionByIdAsync(id);
            return region == null ? NotFound() : Ok(region);
        }
    
        [HttpPost]
        //accpeting request as dto
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDTO regionReq)
        {
           //convert DTO to domain model
           var regionDomain = new Region
           {
               Name = regionReq.Name,
               Code = regionReq.Code,
               RegionImageURL = regionReq.RegionImageURL,
           };
           //add domain region model to database
          await dbContext.Regions.AddAsync(regionDomain);
           await dbContext.SaveChangesAsync();
           
           //map domain model back to dto
           var regionDTO = new RegionDTO()
           {
               RegionImageURL = regionDomain.RegionImageURL,
               Id = regionDomain.Id,
               Code = regionDomain.Code,
               Name = regionDomain.Name
           };
           return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id,[FromBody] UpdateRegionDTO regionReq)
        {
            var region = dbContext.Regions.Find(id);
            if (region == null)
                return NotFound();
            
            //convert DTO to domain model
            region.RegionImageURL = regionReq.RegionImageURL;
            region.Code = regionReq.Code;
            region.Name = regionReq.Name;
            
            //add domain model regionto database
            dbContext.SaveChanges();
            
            //conver domain model to DTO
            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageURL = region.RegionImageURL,
            };
            
            return Ok(regionDTO);

        }
        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.Find(id);
            if(regionDomain == null)
                return NotFound();
            
            dbContext.Regions.Remove(regionDomain);
            dbContext.SaveChanges();
            
            return Ok();
        }
    }
}
