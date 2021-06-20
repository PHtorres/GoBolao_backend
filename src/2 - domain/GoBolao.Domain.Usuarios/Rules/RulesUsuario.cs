using GoBolao.Domain.Shared.Interfaces.Rules;
using GoBolao.Domain.Shared.Interfaces.Service;
using GoBolao.Domain.Shared.Rules;
using GoBolao.Domain.Usuarios.DTO;
using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Domain.Usuarios.Rules
{
    public class RulesUsuario : RulesBase, IRulesUsuario
    {
        private readonly IRepositoryUsuario RepositorioUsuario;
        private readonly IServiceCriptografia ServicoCriptografia;

        public RulesUsuario(IRepositoryUsuario repositorioUsuario, IServiceCriptografia servicoCriptografia)
        {
            RepositorioUsuario = repositorioUsuario;
            ServicoCriptografia = servicoCriptografia;
        }

        public bool AptoParaAlterar(AlterarUsuarioDTO alterarUsuarioDTO, int idUsuario)
        {
            UsuarioDeveExistir(idUsuario);
            EmailDeveSerUnicoNaAlteracao(alterarUsuarioDTO.Email, idUsuario);
            ApelidoDeveSerUnicoNaAlteracao(alterarUsuarioDTO.Apelido, idUsuario);
            return SemFalhas;
        }

        public bool AptoParaCriar(CriarUsuarioDTO criarUsuarioDTO)
        {
            EmailDeveSerUnicoNaCriacao(criarUsuarioDTO.Email);
            ApelidoDeveSerUnicoNaCriacao(criarUsuarioDTO.Apelido);
            SenhaDeveSerConfirmada(criarUsuarioDTO.Senha, criarUsuarioDTO.ConfirmaSenha);
            return SemFalhas;
        }

        public bool AptoParaRemover(int idUsuarioRemover)
        {
            UsuarioDeveExistir(idUsuarioRemover);
            return SemFalhas;
        }

        public bool AptoParaAlterarSenha(AlterarSenhaDTO alterarSenhaDTO, int idUsuario)
        {
            UsuarioDeveExistir(idUsuario);
            SenhaAtualUsuarioDeveSerValida(idUsuario, alterarSenhaDTO.SenhaAtual);
            SenhaDeveSerConfirmada(alterarSenhaDTO.NovaSenha, alterarSenhaDTO.ConfirmaSenha);
            return SemFalhas;
        }

        public void Dispose()
        {
            RepositorioUsuario.Dispose();
            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<string> ObterFalhas()
        {
            return Falhas;
        }

        private void EmailDeveSerUnicoNaCriacao(string email)
        {
            var outrosEmails = RepositorioUsuario.ObterUsuariosPorEmail(email).ToList();
            if (outrosEmails.Any())
            {
                AdicionarFalha("E-mail já em uso. Informa outro, por favor.");
            }
        }

        private void SenhaDeveSerConfirmada(string senha, string confirmaSenha)
        {
            if(senha != confirmaSenha)
            {
                AdicionarFalha("Senhas não conferem.");
            }
        }

        private void ApelidoDeveSerUnicoNaCriacao(string apelido)
        {
            var outrosApelidos = RepositorioUsuario.ObterUsuariosPeloApelido(apelido).ToList();
            if (outrosApelidos.Any())
            {
                AdicionarFalha("Apelido já em uso. Informa outro, por favor.");
            }
        }

        private void UsuarioDeveExistir(int idUsuario)
        {
            var usuario = RepositorioUsuario.Obter(idUsuario);
            if(usuario == null)
            {
                AdicionarFalha("Usuário não existe.");
            }
        }


        private void ApelidoDeveSerUnicoNaAlteracao(string apelido, int idUsuario)
        {
            var outrosApelidos = RepositorioUsuario.ObterUsuariosPeloApelido(apelido).ToList().Where(u => u.Id != idUsuario);
            if (outrosApelidos.Any())
            {
                AdicionarFalha("Apelido já em uso. Informa outro, por favor.");
            }
        }

        private void EmailDeveSerUnicoNaAlteracao(string email, int idUsuario)
        {
            var usuariosComEsteEmail = RepositorioUsuario.ObterUsuariosPorEmail(email).ToList();
            var outrosUsuariosComEsteEmail = usuariosComEsteEmail.Where(u => u.Id != idUsuario);
            if (outrosUsuariosComEsteEmail.Any())
            {
                AdicionarFalha("E-mail já em uso. Informa outro, por favor.");
            }
        }

        private void SenhaAtualUsuarioDeveSerValida(int idUsuario, string senha)
        {
            var usuario = RepositorioUsuario.Obter(idUsuario);
            if(!ServicoCriptografia.ConfereCriptografia(senha, usuario.Senha))
            {
                AdicionarFalha("Senha atual está inválida.");
            }
        }
    }
}
