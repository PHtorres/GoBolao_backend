using GoBolao.Domain.Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GoBolao.Service.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize]
    public class CampeonatoController : SharedController
    {
        private readonly IServiceCampeonato ServicoCampeonato;
        public CampeonatoController(IHttpContextAccessor _httpContextAccessor, IServiceCampeonato servicoCampeonato = null) : base(_httpContextAccessor)
        {
            ServicoCampeonato = servicoCampeonato;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CampeonatoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CampeonatoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CampeonatoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CampeonatoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
