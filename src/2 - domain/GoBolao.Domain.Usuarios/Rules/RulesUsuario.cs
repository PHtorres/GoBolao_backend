using GoBolao.Domain.Shared.Interfaces.Rules;
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

        public RulesUsuario(IRepositoryUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
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
            var outrosEmails = RepositorioUsuario.ObterUsuariosPorEmail(email).ToList().Where(u => u.Id != idUsuario);
            if (outrosEmails.Any())
            {
                AdicionarFalha("E-mail já em uso. Informa outro, por favor.");
            }
        }
    }
}
