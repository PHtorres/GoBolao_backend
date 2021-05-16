using GoBolao.Domain.Shared.Interfaces.Rules;
using GoBolao.Domain.Usuarios.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Usuarios.Interfaces.Rules
{
    public interface IRulesUsuario:IRules
    {
        bool AptoParaCriar(CriarUsuarioDTO criarUsuarioDTO);
        bool AptoParaAlterar(AlterarUsuarioDTO alterarUsuarioDTO, int idUsuario);
        bool AptoParaRemover(int idUsuarioRemover);
    }
}
