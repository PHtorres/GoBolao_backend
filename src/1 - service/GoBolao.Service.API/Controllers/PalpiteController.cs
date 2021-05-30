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
    public class PalpiteController : SharedController
    {
        private readonly IServicePalpite ServicoPalpite;
        public PalpiteController(IHttpContextAccessor _httpContextAccessor, IServicePalpite servicoPalpite) : base(_httpContextAccessor)
        {
            ServicoPalpite = servicoPalpite;
        }

        [HttpGet]
        [Route("abertos")]
        public ActionResult<Resposta<IEnumerable<PalpiteDTO>>> GetAbertos()
        {
            return Ok(ServicoPalpite.ObterPalpitesAbertosPorUsuario(IdUsuarioAcao));
        }

        [HttpGet]
        [Route("finalizados")]
        public ActionResult<Resposta<IEnumerable<PalpiteDTO>>> GetFinalizados()
        {
            return Ok(ServicoPalpite.ObterPalpitesFinalizadosPorUsuario(IdUsuarioAcao));
        }

        [HttpGet]
        [Route("jogo/{idJogo}")]
        public ActionResult<Resposta<IEnumerable<PalpiteDTO>>> GetPorJogo(int idJogo)
        {
            return Ok(ServicoPalpite.ObterPalpitesPorJogoFinalizadoOuIniciadoDosAdiversarios(idJogo, IdUsuarioAcao));
        }

        [HttpPost]
        public ActionResult<Resposta<Palpite>> Post([FromBody] CriarPalpiteDTO criarPalpiteDTO)
        {
            return Ok(ServicoPalpite.CriarPalpite(criarPalpiteDTO, IdUsuarioAcao));
        }

        [HttpDelete("{idPalpite}")]
        public ActionResult<Resposta<Palpite>> Delete(int idPalpite)
        {
            return Ok(ServicoPalpite.RemoverPalpite(idPalpite, IdUsuarioAcao));
        }
    }
}
