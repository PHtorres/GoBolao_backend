using GoBolao.Domain.Shared.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Entidades;
using System.Collections.Generic;

namespace GoBolao.Domain.Usuarios.Interfaces.Repository
{
    public interface IRepositoryUsuario:IRepositoryGenerico<Usuario>
    {
        IEnumerable<Usuario> ObterUsuariosPorEmail(string email);
        IEnumerable<Usuario> ObterUsuariosPeloApelido(string apelido);
    }
}
