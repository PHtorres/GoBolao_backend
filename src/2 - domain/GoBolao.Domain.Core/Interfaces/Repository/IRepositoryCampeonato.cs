using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.Interfaces.Repository;
using System.Collections.Generic;

namespace GoBolao.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryCampeonato:IRepositoryGenerico<Campeonato>
    {
        IEnumerable<Campeonato> ObterCampeonatosPeloNome(string nome);
    }
}
