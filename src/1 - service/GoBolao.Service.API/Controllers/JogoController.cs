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
    public class JogoController : SharedController
    {
        private readonly IServiceJogo ServicoJogo;
        public JogoController(IHttpContextAccessor _httpContextAccessor, IServiceJogo servicoJogo) : base(_httpContextAccessor)
        {
            ServicoJogo = servicoJogo;
        }

        [HttpGet]
        public ActionResult<Resposta<IEnumerable<JogoDTO>>> Get()
        {
            return Ok(ServicoJogo.ObterTodosJogos(IdUsuarioAcao));
        }

        [HttpGet]
        [Route("futuros")]
        public ActionResult<Resposta<IEnumerable<JogoDTO>>> GetFuturos()
        {
            return Ok(ServicoJogo.ObterJogosFuturos(IdUsuarioAcao));
        }

        [HttpGet]
        [Route("hoje")]
        public ActionResult<Resposta<IEnumerable<JogoDTO>>> GetHoje()
        {
            return Ok(ServicoJogo.ObterJogosDeHoje(IdUsuarioAcao));
        }

        [HttpGet]
        [Route("amanha")]
        public ActionResult<Resposta<IEnumerable<JogoDTO>>> GetAmanha()
        {
            return Ok(ServicoJogo.ObterJogosDeAmanha(IdUsuarioAcao));
        }

        [HttpPost]
        public ActionResult<Resposta<Jogo>> Post([FromBody] CriarJogoDTO criarJogoDTO)
        {
            return Ok(ServicoJogo.CriarJogo(criarJogoDTO));
        }

        [HttpPatch]
        [Route("finalizar")]
        public ActionResult<Resposta<Jogo>> Patch([FromBody] FinalizarJogoDTO finalizarJogoDTO)
        {
            return Ok(ServicoJogo.FinalizarJogo(finalizarJogoDTO));
        }
    }
}
