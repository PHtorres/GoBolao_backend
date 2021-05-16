using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Infra.Data.Contextos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GoBolao.Infra.Data.Repository
{
    public class RepositoryCampeonato : RepositoryGenerico<Campeonato>, IRepositoryCampeonato
    {
        private readonly ContextoMSSQL Sql;
        private readonly DbSet<Campeonato> DbSetCampeonato;
        public RepositoryCampeonato(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetCampeonato = Sql.Set<Campeonato>();
        }

        public IEnumerable<Campeonato> ObterCampeonatosPeloNome(string nome)
        {
            var campeonatos = DbSetCampeonato.Where(c => c.Nome == nome);
            return campeonatos;
        }
    }
}
