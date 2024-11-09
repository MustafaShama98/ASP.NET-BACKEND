using AutoMapper;
using dotnet.Data;
using dotnet.Models.Domain;
using dotnet.Models.DTO.RegionDTOs;
using dotnet.Repoistry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly WalkDbContext dbContext;
        private readonly IRegionRepositry regionRepositry;
        private readonly IMapper mapper;

        public RegionsController(WalkDbContext dbContext, IRegionRepositry regionRepositry,
            IMapper mapper)
        {
            this.dbContext = dbContext;//not needed anymore becaues of repo pattern 
            this.regionRepositry = regionRepositry;
            this.mapper = mapper;
        }
        
        [HttpGet]
        [Authorize(Roles = "Reader ")]

        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepositry.GetAllRegionsAsync();
            // map domain to dto
            var regionDtos =  mapper.Map<List<RegionDTO>>(regionsDomain);
            return Ok(regionDtos);
        }
        
        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = await regionRepositry.GetRegionByIdAsync(id);
            return region == null ? NotFound() : Ok(mapper.Map<RegionDTO>(region));
        }
    
        [HttpPost]
        [Authorize(Roles = "Writer")]
        //accpeting request as dto
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDTO regionReq)
        {
           //convert DTO to domain model
           // var regionDomain = new Region
           // {
           //     Name = regionReq.Name,
           //     Code = regionReq.Code,
           //     RegionImageURL = regionReq.RegionImageURL,
           // };
           
           var regionDomain = mapper.Map<Region>(regionReq);
           //add domain region model to database
           await regionRepositry.CreateRegionAsync(regionDomain);
           
           //map domain model back to dto
           // var regionDTO = new RegionDTO()
           // {
           //     RegionImageURL = regionDomain.RegionImageURL,
           //     Id = regionDomain.Id,
           //     Code = regionDomain.Code,
           //     Name = regionDomain.Name
           // };
           var regionDTO = mapper.Map<RegionDTO>(regionDomain);
           return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id,[FromBody] UpdateRegionDTO regionReq)
        
        {
            //map DTO to domian model

            // var regionDomainModel = new Region()
            // {
            //     Code = regionReq.Code,
            //     Name = regionReq.Name,
            //     RegionImageURL = regionReq.RegionImageURL,
            // };
            var regionDomainModel = mapper.Map<Region>(regionReq);
            var region = await regionRepositry.UpdateRegionAsync(id, regionDomainModel);
            if (region == null)
                return NotFound();
            
            
            
            //conver domain model to DTO
            // var regionDTO = new RegionDTO()
            // {
            //     Id = region.Id,
            //     Name = region.Name,
            //     Code = region.Code,
            //     RegionImageURL = region.RegionImageURL,
            // };
            var regionDTO = mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);

        }
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = await regionRepositry.DeleteRegionAsync(id);
            if(regionDomain == null)
                return NotFound();
            
            
            return Ok();
        }
    }
}
