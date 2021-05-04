using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Usuarios.DTO;
using System;

namespace GoBolao.Domain.Usuarios.Interfaces.Service
{
    public interface IServiceAutenticacao:IDisposable
    {
        Resposta<UsuarioDTO> AutenticarUsuario(AutenticarUsuarioDTO autenticarUsuarioDTO);
    }
}
