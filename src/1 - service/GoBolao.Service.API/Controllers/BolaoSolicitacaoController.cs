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
    public class BolaoSolicitacaoController : SharedController
    {
        private readonly IServiceBolaoSolicitacao ServicoBolaoSolicitacao;
        public BolaoSolicitacaoController(IHttpContextAccessor _httpContextAccessor, IServiceBolaoSolicitacao servicoBolaoSolicitacao) : base(_httpContextAccessor)
        {
            ServicoBolaoSolicitacao = servicoBolaoSolicitacao;
        }


        [HttpGet("{idBolao}")]
        public ActionResult<Resposta<IEnumerable<BolaoSolicitacaoDTO>>> Get(int idBolao)
        {
            return Ok(ServicoBolaoSolicitacao.ObterSolicitacoesPorBolao(idBolao, IdUsuarioAcao));
        }

        [HttpPost]
        public ActionResult<Resposta<BolaoSolicitacao>> Post([FromBody] CriarBolaoSolicitacaoDTO criarBolaoSolicitacaoDTO)
        {
            return Ok(ServicoBolaoSolicitacao.CriarSolicitacao(criarBolaoSolicitacaoDTO, IdUsuarioAcao));
        }

        [HttpPatch]
        [Route("aceitar")]
        public ActionResult<Resposta<BolaoSolicitacao>> PatchAceitar([FromBody] AceitarBolaoSolicitacaoDTO aceitarBolaoSolicitacaoDTO)
        {
            return Ok(ServicoBolaoSolicitacao.AceitarSolicitacao(aceitarBolaoSolicitacaoDTO, IdUsuarioAcao));
        }

        [HttpPatch]
        [Route("recusar")]
        public ActionResult<Resposta<BolaoSolicitacao>> PatchRecusar([FromBody] RecusarBolaoSolicitacaoDTO recusarBolaoSolicitacaoDTO)
        {
            return Ok(ServicoBolaoSolicitacao.RecusarSolicitacao(recusarBolaoSolicitacaoDTO, IdUsuarioAcao));
        }

        [HttpDelete("{idSolicitacao}")]
        public ActionResult<Resposta<BolaoSolicitacao>> Delete(int idSolicitacao)
        {
            return Ok(ServicoBolaoSolicitacao.DesfazerSolicitacao(idSolicitacao, IdUsuarioAcao));
        }
    }
}
