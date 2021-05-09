using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Infra.Data.Contextos;

namespace GoBolao.Infra.Data.Repository
{
    public class RepositoryCampeonato : RepositoryGenerico<Campeonato>, IRepositoryCampeonato
    {
        public RepositoryCampeonato(ContextoMSSQL _sql) : base(_sql)
        {
        }
    }
}
