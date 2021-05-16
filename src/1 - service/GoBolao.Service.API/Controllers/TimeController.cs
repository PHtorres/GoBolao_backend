using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Shared.DomainObjects;
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
    public class TimeController : SharedController
    {
        private readonly IServiceTime ServicoTime;
        public TimeController(IHttpContextAccessor _httpContextAccessor, IServiceTime servicoTime) : base(_httpContextAccessor)
        {
            ServicoTime = servicoTime;
        }

        [HttpGet]
        public ActionResult<Resposta<IEnumerable<Time>>> Get()
        {
            return Ok(ServicoTime.ObterTimes());
        }

        [HttpPost]
        public ActionResult<Resposta<Time>> Post([FromBody] CriarTimeDTO criarTimeDTO)
        {
            return Ok(ServicoTime.CriarTime(criarTimeDTO));
        }

        [HttpPut]
        public ActionResult<Resposta<Time>> Put([FromBody] AlterarTimeDTO alterarTimeDTO)
        {
            return Ok(ServicoTime.AlterarTime(alterarTimeDTO));
        }
    }
}
