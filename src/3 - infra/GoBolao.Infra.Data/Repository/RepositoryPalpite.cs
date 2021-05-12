﻿using Dapper;
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
            var palpites = DbSetPalpite.Where(p => p.IdJogo == idJogo).AsEnumerable();
            return palpites;
        }

        public IEnumerable<PalpiteDTO> ObterPalpitesPorUsuario(int idUsuario)
        {
            var query = @"SELECT 
                         (SELECT NOME FROM TIME T WHERE T.Id = J.IdMandante) Mandante,
                         (SELECT NOME FROM TIME T WHERE T.Id = J.IdVisitante) Visitante,
                         (SELECT NomeImagemAvatar FROM TIME T WHERE T.Id = J.IdMandante) NomeImagemAvatarMandante,
                         (SELECT NomeImagemAvatar FROM TIME T WHERE T.Id = J.IdVisitante) NomeImagemAvatarVisitante,
                         p.DataHora,
                         p.PlacarMandantePalpite,
                         p.PlacarVisitantePalpite,
                         p.PlacarMandanteReal,
                         p.PlacarVisitanteReal,
                         p.Pontos,
                         p.Finalizado
                         FROM
                         PALPITE P,
                         JOGO J
                         WHERE 
                         P.IdUsuario = @ID_USUARRIO AND
                         P.IdJogo = J.Id";

            var palpites = Sql.Database.GetDbConnection().Query<PalpiteDTO>(query, new { ID_USUARRIO = idUsuario });
            return palpites;
        }
    }
}
