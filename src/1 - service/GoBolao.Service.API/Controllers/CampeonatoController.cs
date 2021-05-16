using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
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
        public ActionResult<IEnumerable<Campeonato>> Get()
        {
            return Ok(ServicoCampeonato.ObterCampeonatos());
        }


        [HttpPost]
        public ActionResult<Campeonato> Post([FromBody] CriarCampeonatoDTO criarCampeonatoDTO)
        {
            return Ok(ServicoCampeonato.CriarCampeonato(criarCampeonatoDTO));
        }

        [HttpPatch]
        [Route("avatar")]
        public ActionResult<Campeonato> Patch([FromBody] AlterarNomeImagemAvatarCampeonatoDTO alterarNomeImagemAvatarCampeonatoDTO)
        {
            return Ok(ServicoCampeonato.AlterarNomeImagemAvatar(alterarNomeImagemAvatarCampeonatoDTO));
        }
    }
}
