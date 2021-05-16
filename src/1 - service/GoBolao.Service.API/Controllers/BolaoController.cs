using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Shared.DomainObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoBolao.Service.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize]
    public class BolaoController : SharedController
    {
        private readonly IServiceBolao ServicoBolao;
        public BolaoController(IHttpContextAccessor _httpContextAccessor, IServiceBolao servicoBolao) : base(_httpContextAccessor)
        {
            ServicoBolao = servicoBolao;
        }


        [HttpGet("{idBolao}")]
        public ActionResult<Resposta<BolaoDTO>> Get(int idBolao)
        {
            return Ok(ServicoBolao.ObterBolaoPorId(idBolao));
        }

        [HttpGet]
        [Route("pesquisa/{pesquisa}")]
        public ActionResult<Resposta<BolaoDTO>> GetPesquisa(string pesquisa)
        {
            return Ok(ServicoBolao.PesquisarBoloes(pesquisa));
        }

        [HttpPost]
        public ActionResult<Resposta<Bolao>> Post([FromBody] CriarBolaoDTO criarBolaoDTO)
        {
            return Ok(ServicoBolao.CriarBolao(criarBolaoDTO, IdUsuarioAcao));
        }


        [HttpPatch]
        [Route("avatar")]
        public ActionResult<Resposta<Bolao>> Patch([FromBody] AlterarNomeImagemAvatarBolaoDTO alterarNomeImagemAvatarBolaoDTO)
        {
            return Ok(ServicoBolao.AlterarNomeImagemAvatar(alterarNomeImagemAvatarBolaoDTO, IdUsuarioAcao));
        }
    }
}
