using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Usuarios.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Usuarios.Interfaces.Service
{
    public interface IServiceUsuario:IDisposable
    {
        Resposta<UsuarioDTO> CriarUsuario(CriarUsuarioDTO criarUsuarioDTO);
        Resposta<UsuarioDTO> AlterarUsuario(AlterarUsuarioDTO alterarUsuarioDTO, int idUsuarioAcao);
        Resposta<UsuarioDTO> ObterUsuarioPeloId(int IdUsuario);
        Resposta<UsuarioDTO> RemoverUsuario(int IdUsuarioAcao);
        Resposta<IEnumerable<UsuarioDTO>> ObterUsuarios();
        Resposta<UsuarioDTO> AlterarNomeImagemAvatar(AlterarNomeImagemAvatarDTO alterarNomeImagemAvatarDTO, int idUsuarioAcao);
    }
}
