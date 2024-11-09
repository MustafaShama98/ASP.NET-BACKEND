using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StamControler : ControllerBase
    {
        // GET: api/<StamControler>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StamControler>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StamControler>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StamControler>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StamControler>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
