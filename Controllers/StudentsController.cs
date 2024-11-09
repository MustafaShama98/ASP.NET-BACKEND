using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace dotnet.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // api/studentsr
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(new string[] { "'", "erfgfg" });
        }
    }