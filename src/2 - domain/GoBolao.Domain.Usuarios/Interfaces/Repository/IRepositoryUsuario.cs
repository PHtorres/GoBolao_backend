using GoBolao.Domain.Shared.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Entidades;

namespace GoBolao.Domain.Usuarios.Interfaces.Repository
{
    public interface IRepositoryUsuario:IRepositoryGenerico<Usuario>
    {
        Usuario ObterUsuarioPorEmail(string email);
    }
}
