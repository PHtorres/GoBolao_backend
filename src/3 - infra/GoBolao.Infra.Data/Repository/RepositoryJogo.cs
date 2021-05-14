using Dapper;
using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Infra.Data.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBolao.Infra.Data.Repository
{
    public class RepositoryJogo : RepositoryGenerico<Jogo>, IRepositoryJogo
    {
        private ContextoMSSQL Sql;
        private DbSet<Jogo> DbSetJogo;

        public RepositoryJogo(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetJogo = Sql.Set<Jogo>();
        }

        public IEnumerable<JogoDTO> ObterJogos(int idUsuario)
        {
            var query = @"SELECT 
                          J.Id,
                          C.Nome NomeCampeonato,
                          j.DataHora,
                          (SELECT Nome from TIME T WHERE T.Id = J.IdMandante) Mandante,
                          (SELECT Nome from TIME T WHERE T.Id = J.IdVisitante) Visitante,
                          (SELECT NomeImagemAvatar from TIME T WHERE T.Id = J.IdMandante) NomeImagemAvatarMandante,
                          (SELECT NomeImagemAvatar from TIME T WHERE T.Id = J.IdVisitante) NomeImagemAvatarVisitante,
                          j.PlacarMandante,
                          j.PlacarVisitante,
                          j.Fase,
                          (SELECT 1 FROM PALPITE P WHERE p.IdJogo = j.Id and p.IdUsuario = @ID_USUARIO) UsuarioTemPalpite
                          FROM
                          JOGO J,
                          CAMPEONATO C
                          WHERE
                          J.IdCampeonato = C.Id";

            var jogos = Sql.Database.GetDbConnection().Query<JogoDTO>(query, new { ID_USUARIO = idUsuario });
            return jogos;
        }

        public IEnumerable<JogoDTO> ObterJogosNaData(DateTime data, int idUsuario)
        {
            var query = @"SELECT
                          J.Id,
                          C.Nome NomeCampeonato,
                          j.DataHora,
                          (SELECT Nome from TIME T WHERE T.Id = J.IdMandante) Mandante,
                          (SELECT Nome from TIME T WHERE T.Id = J.IdVisitante) Visitante,
                          (SELECT NomeImagemAvatar from TIME T WHERE T.Id = J.IdMandante) NomeImagemAvatarMandante,
                          (SELECT NomeImagemAvatar from TIME T WHERE T.Id = J.IdVisitante) NomeImagemAvatarVisitante,
                          j.PlacarMandante,
                          j.PlacarVisitante,
                          j.Fase,
                          (SELECT 1 FROM PALPITE P WHERE p.IdJogo = j.Id and p.IdUsuario = @ID_USUARIO) UsuarioTemPalpite
                          FROM
                          JOGO J,
                          CAMPEONATO C
                          WHERE
                          CONVERT(date, j.DataHora) = @DATA AND
                          J.IdCampeonato = C.Id";

            var jogos = Sql.Database.GetDbConnection().Query<JogoDTO>(query, new { ID_USUARIO = idUsuario, DATA = data.Date });
            return jogos;
        }
    }
}
