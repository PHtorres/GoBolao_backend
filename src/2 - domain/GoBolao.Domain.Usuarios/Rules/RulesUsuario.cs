using GoBolao.Domain.Shared.Interfaces.Rules;
using GoBolao.Domain.Shared.Rules;
using GoBolao.Domain.Usuarios.DTO;
using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Interfaces.Rules;
using System;
using System.Collections.Generic;
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

        public bool AptoParaAlterar(AlterarUsuarioDTO alterarUsuarioDTO)
        {
            var outroUsuarioComMesmoEmail = RepositorioUsuario.ObterUsuarioPorEmail(alterarUsuarioDTO.Email);
            if(outroUsuarioComMesmoEmail != null)
            {

            }
        }

        public bool AptoParaCriar(CriarUsuarioDTO criarUsuarioDTO)
        {
            throw new NotImplementedException();
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
    }
}
