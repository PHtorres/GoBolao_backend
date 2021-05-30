using Dapper;
using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Infra.Data.Contextos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GoBolao.Infra.Data.Repository
{
    public class RepositoryPalpite : RepositoryGenerico<Palpite>, IRepositoryPalpite
    {
        private ContextoMSSQL Sql;
        private DbSet<Palpite> DbSetPalpite;

        public RepositoryPalpite(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetPalpite = Sql.Set<Palpite>();
        }

        public IEnumerable<Palpite> ObterPalpitesPorJogo(int idJogo)
        {
            var palpites = DbSetPalpite.Where(p => p.IdJogo == idJogo);
            return palpites;
        }

        public IEnumerable<PalpiteDTO> ObterPalpitesPorJogoDTO(int idJogo)
        {
            var query = @"SELECT
                         p.Id,
                         j.Id IdJogo,
                         U.Id IdUsuarioPalpite,
                         U.Apelido NomeUsuarioPalpite,
                         (SELECT NOME FROM TIME T WHERE T.Id = J.IdMandante) Mandante,
                         (SELECT NOME FROM TIME T WHERE T.Id = J.IdVisitante) Visitante,
                         (SELECT NomeImagemAvatar FROM TIME T WHERE T.Id = J.IdMandante) NomeImagemAvatarMandante,
                         (SELECT NomeImagemAvatar FROM TIME T WHERE T.Id = J.IdVisitante) NomeImagemAvatarVisitante,
                         p.DataHora DataHoraPalpite,
                         j.DataHora DataHoraJogo,
                         p.PlacarMandantePalpite,
                         p.PlacarVisitantePalpite,
                         J.PlacarMandante PlacarMandanteReal,
                         J.PlacarVisitante PlacarVisitanteReal,
                         p.Pontos,
                         p.Finalizado
                         FROM
                         PALPITE P,
                         JOGO J,
                         USUARIO U
                         WHERE 
                         P.IdJogo = J.Id AND
                         P.IdUsuario = U.Id AND
                         P.IdJogo = @IDJOGO
                         ORDER BY p.DataHora DESC";

            var palpites = Sql.Database.GetDbConnection().Query<PalpiteDTO>(query, new { IDJOGO = idJogo });
            return palpites;
        }

        public IEnumerable<PalpiteDTO> ObterPalpitesPorUsuario(int idUsuario)
        {
            var query = @"SELECT
                         p.Id,
                         j.Id IdJogo,
                         U.Id IdUsuarioPalpite,
                         U.Apelido NomeUsuarioPalpite,
                         (SELECT NOME FROM TIME T WHERE T.Id = J.IdMandante) Mandante,
                         (SELECT NOME FROM TIME T WHERE T.Id = J.IdVisitante) Visitante,
                         (SELECT NomeImagemAvatar FROM TIME T WHERE T.Id = J.IdMandante) NomeImagemAvatarMandante,
                         (SELECT NomeImagemAvatar FROM TIME T WHERE T.Id = J.IdVisitante) NomeImagemAvatarVisitante,
                         p.DataHora DataHoraPalpite,
                         j.DataHora DataHoraJogo,
                         p.PlacarMandantePalpite,
                         p.PlacarVisitantePalpite,
                         J.PlacarMandante PlacarMandanteReal,
                         J.PlacarVisitante PlacarVisitanteReal,
                         p.Pontos,
                         p.Finalizado
                         FROM
                         PALPITE P,
                         JOGO J,
                         USUARIO U
                         WHERE 
                         P.IdUsuario = @ID_USUARIO AND
                         P.IdJogo = J.Id AND
                         P.IdUsuario = U.Id
                         ORDER BY p.DataHora DESC";

            var palpites = Sql.Database.GetDbConnection().Query<PalpiteDTO>(query, new { ID_USUARIO = idUsuario });
            return palpites;
        }
    }
}
