using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Usuarios.DTO;
using GoBolao.Domain.Usuarios.Entidades;
using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Interfaces.Service;
using GoBolao.Domain.Usuarios.ManualMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Domain.Usuarios.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario RepositorioUsuario;
        private Resposta<UsuarioDTO> Resposta;

        public ServiceUsuario(IRepositoryUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
            Resposta = new Resposta<UsuarioDTO>();
        }

        public Resposta<UsuarioDTO> AlterarUsuario(AlterarUsuarioDTO alterarUsuarioDTO, int idUsuarioAcao)
        {
            var usuario = RepositorioUsuario.Obter(alterarUsuarioDTO.Id);
            usuario.AlterarApelido(alterarUsuarioDTO.Apelido);
            usuario.AlterarEmail(alterarUsuarioDTO.Email);

            if (usuario.Invalido)
            {
                Resposta.AdicionarNotificacao(usuario._Erros);
                return Resposta;
            }

            RepositorioUsuario.Atualizar(usuario);
            RepositorioUsuario.Salvar();

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }

        public Resposta<UsuarioDTO> CriarUsuario(CriarUsuarioDTO criarUsuarioDTO)
        {
            var usuario = new Usuario(criarUsuarioDTO.Apelido, criarUsuarioDTO.Email, criarUsuarioDTO.Senha);

            if (usuario.Invalido)
            {
                Resposta.AdicionarNotificacao(usuario._Erros);
                return Resposta;
            }

            RepositorioUsuario.Adicionar(usuario);
            RepositorioUsuario.Salvar();

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }

        public void Dispose()
        {
            RepositorioUsuario.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<UsuarioDTO> ObterUsuarioPeloId(int IdUsuario)
        {
            var usuario = RepositorioUsuario.Obter(IdUsuario);
            if(usuario == null)
            {
                Resposta.AdicionarNotificacao("Usuário inexistente.");
                return Resposta;
            }

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }

        public Resposta<IEnumerable<UsuarioDTO>> ObterUsuarios()
        {
            var respostaLista = new Resposta<IEnumerable<UsuarioDTO>>();

            var usuarios = RepositorioUsuario.Listar().Select(item => item.UsuarioParaDTO());

            respostaLista.AdicionarConteudo(usuarios);
            return respostaLista;
        }

        public Resposta<UsuarioDTO> RemoverUsuario(int idUsuario, int IdUsuarioAcao)
        {
            var usuario = RepositorioUsuario.Obter(idUsuario);
            if (usuario == null)
            {
                Resposta.AdicionarNotificacao("Usuário inexistente.");
                return Resposta;
            }

            RepositorioUsuario.Remover(usuario);
            RepositorioUsuario.Salvar();

            return Resposta;
        }
    }
}
