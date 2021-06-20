using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Usuarios.DTO;
using GoBolao.Domain.Usuarios.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace GoBolao.Service.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize]
    public class UsuarioController : SharedController
    {
        private readonly IServiceUsuario ServicoUsuario;

        public UsuarioController(IHttpContextAccessor _httpContextAccessor, IServiceUsuario servicoUsuario) : base(_httpContextAccessor)
        {
            ServicoUsuario = servicoUsuario;
        }

        [HttpGet]
        public ActionResult<Resposta<IEnumerable<UsuarioDTO>>> Get()
        {
            return Ok(ServicoUsuario.ObterUsuarios());
        }

        [HttpGet("{id}")]
        public ActionResult<Resposta<UsuarioDTO>> Get(int id)
        {
            return Ok(ServicoUsuario.ObterUsuarioPeloId(id));
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Resposta<UsuarioDTO>> Post([FromBody] CriarUsuarioDTO criarUsuarioDTO)
        {
            return Ok(ServicoUsuario.CriarUsuario(criarUsuarioDTO));
        }

        [HttpPut]
        public ActionResult<Resposta<UsuarioDTO>> Put([FromBody] AlterarUsuarioDTO alterarUsuarioDTO)
        {
            return Ok(ServicoUsuario.AlterarUsuario(alterarUsuarioDTO, IdUsuarioAcao));
        }

        [HttpPatch]
        [Route("avatar")]
        public ActionResult<Resposta<UsuarioDTO>> PatchAvatar([FromBody] AlterarNomeImagemAvatarDTO alterarNomeImagemAvatarDTO)
        {
            return Ok(ServicoUsuario.AlterarNomeImagemAvatar(alterarNomeImagemAvatarDTO, IdUsuarioAcao));
        }

        [HttpPatch]
        [Route("senha")]
        public ActionResult<Resposta<UsuarioDTO>> PatchSenha([FromBody] AlterarSenhaDTO alterarSenhaDTO)
        {
            return Ok(ServicoUsuario.AlterarSenha(alterarSenhaDTO, IdUsuarioAcao));
        }

        [HttpDelete]
        public ActionResult<Resposta<UsuarioDTO>> Delete()
        {
            return Ok(ServicoUsuario.RemoverUsuario(IdUsuarioAcao));
        }
    }
}
