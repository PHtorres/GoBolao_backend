using GoBolao.Domain.Usuarios.DTO;
using GoBolao.Domain.Usuarios.Interfaces.Service;
using GoBolao.Service.API.Models;
using GoBolao.Service.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GoBolao.Service.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class AutenticacaoController : SharedController
    {
        private readonly IServiceAutenticacao ServicoAutenticacao;
        public AutenticacaoController(IHttpContextAccessor _httpContextAccessor, IServiceAutenticacao servicoAutenticacao) : base(_httpContextAccessor)
        {
            ServicoAutenticacao = servicoAutenticacao;
        }

        [HttpPost]
        public ActionResult<RespostaAutenticacao> Post([FromBody] AutenticarUsuarioDTO autenticarUsuarioDTO)
        {
            try
            {
                var resposta = ServicoAutenticacao.AutenticarUsuario(autenticarUsuarioDTO);

                if (resposta.Sucesso)
                {
                    return Ok(new RespostaAutenticacao
                    {
                        Token = GerarTokenUsuario(resposta.Conteudo.Id),
                        Resposta = resposta
                    });
                }


                return Ok(new RespostaAutenticacao
                {
                    Token = null,
                    Resposta = resposta
                });
            }
            catch(Exception e)
            {
                return Ok($@"{e.Messege}")
            }
           
        }

        private string GerarTokenUsuario(int idUsuario)
        {
            return TokenService.GerarJsonWebToken("id_usuario", idUsuario.ToString());
        }
    }
}
