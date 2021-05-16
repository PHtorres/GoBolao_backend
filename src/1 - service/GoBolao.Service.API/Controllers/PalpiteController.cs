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
        public ActionResult<Resposta<IEnumerable<PalpiteDTO>>> Get()
        {
            return Ok(ServicoPalpite.ObterPalpitesPorUsuario(IdUsuarioAcao));
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
