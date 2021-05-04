using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Shared.Interfaces.Service;
using GoBolao.Domain.Usuarios.DTO;
using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Interfaces.Service;
using GoBolao.Domain.Usuarios.ManualMapper;
using System;

namespace GoBolao.Domain.Usuarios.Services
{
    public class ServiceAutenticacao : IServiceAutenticacao
    {
        private readonly IRepositoryUsuario RepositorioUsuario;
        private readonly IServiceCriptografia Criptografia;
        private Resposta<UsuarioDTO> Resposta;

        public ServiceAutenticacao(IRepositoryUsuario repositorioUsuario, IServiceCriptografia criptografia)
        {
            RepositorioUsuario = repositorioUsuario;
            Criptografia = criptografia;
            Resposta = new Resposta<UsuarioDTO>();
        }

        public Resposta<UsuarioDTO> AutenticarUsuario(AutenticarUsuarioDTO autenticarUsuarioDTO)
        {
            var usuario = RepositorioUsuario.ObterUsuarioPorEmail(autenticarUsuarioDTO.Email);

            if(usuario == null)
            {
                Resposta.AdicionarNotificacao("Usuário inexistente.");
                return Resposta;
            }

            if(!Criptografia.ConfereCriptografia(autenticarUsuarioDTO.Senha, usuario.Senha))
            {
                Resposta.AdicionarNotificacao("Senha incorreta.");
                return Resposta;
            }

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }

        public void Dispose()
        {
            RepositorioUsuario.Dispose();
            Criptografia.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
