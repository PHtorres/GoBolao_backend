using Dapper;
using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Entidades;
using GoBolao.Infra.Data.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBolao.Infra.Data.Repository
{
    public class RepositoryBolao : RepositoryGenerico<Bolao>, IRepositoryBolao
    {
        private ContextoMSSQL Sql;
        private DbSet<Bolao> DbSetBolao;
        private DbSet<Usuario> DbSetUsuario;
        private DbSet<Campeonato> DbSetCampeonato;
        private DbSet<BolaoUsuario> DbSetBolaoUsuario;

        public RepositoryBolao(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetBolao = Sql.Set<Bolao>();
            DbSetUsuario = Sql.Set<Usuario>();
            DbSetCampeonato = Sql.Set<Campeonato>();
            DbSetBolaoUsuario = Sql.Set<BolaoUsuario>();
        }

        public BolaoDTO ObterBolaoPorId(int idBolao)
        {
            var bolaoDTO = DbSetBolao.Where(bolao => bolao.Id == idBolao)
                                     .Join(DbSetUsuario, b => b.IdCriador, u => u.Id,
                                     (b, u) => new
                                     {
                                         IdBolao = b.Id,
                                         IdCampeonato = b.IdCampeonato,
                                         Nome = b.Nome,
                                         NomeCriador = u.Apelido,
                                         NomeImagemAvatar = b.NomeImagemAvatar,
                                         Privacidade = b.Privacidade.ToString()
                                     })
                                     .Join(DbSetCampeonato, b => b.IdCampeonato, c => c.Id,
                                     (b, c) => new BolaoDTO
                                     {
                                         IdBolao = b.IdBolao,
                                         Nome = b.Nome,
                                         NomeCampeonato = c.Nome,
                                         NomeCriador = b.NomeCriador,
                                         NomeImagemAvatarBolao = b.NomeImagemAvatar,
                                         Privacidade = b.Privacidade,
                                         NomeImagemAvatarCampeonato = c.NomeImagemAvatar
                                     }).FirstOrDefault();

            return bolaoDTO;
        }

        public IEnumerable<Bolao> ObterBoloesPeloNome(string nome)
        {
            var boloes = DbSetBolao.Where(b => b.Nome == nome);
            return boloes;
        }

        public IEnumerable<BolaoDTO> ObterBoloesPesquisa(string pesquisa)
        {
            var listaBolaoDTO = DbSetBolao.Where(bolao => bolao.Nome.Contains(pesquisa))
                         .Join(DbSetUsuario, b => b.IdCriador, u => u.Id,
                         (b, u) => new
                         {
                             IdBolao = b.Id,
                             IdCampeonato = b.IdCampeonato,
                             Nome = b.Nome,
                             NomeCriador = u.Apelido,
                             NomeImagemAvatar = b.NomeImagemAvatar,
                             Privacidade = b.Privacidade.ToString()
                         })
                         .Join(DbSetCampeonato, b => b.IdCampeonato, c => c.Id,
                         (b, c) => new BolaoDTO
                         {
                             IdBolao = b.IdBolao,
                             Nome = b.Nome,
                             NomeCampeonato = c.Nome,
                             NomeCriador = b.NomeCriador,
                             NomeImagemAvatarBolao = b.NomeImagemAvatar,
                             Privacidade = b.Privacidade,
                             NomeImagemAvatarCampeonato = c.NomeImagemAvatar
                         }).AsEnumerable();

            return listaBolaoDTO;
        }

        public IEnumerable<ItemRankingBolaoDTO> ObterClassificacaoRankingBolao(int idBolao)
        {
            var query = @"SELECT 
                          U.Apelido ApelidoUsuario,
                          SUM(P.Pontos) Pontos,
                          COUNT(P.Id) QuantidadePalpites
                          FROM
                          PALPITE P,
                          BOLAO_USUARIO BU,
                          USUARIO U,
                          CAMPEONATO C,
                          BOLAO B,
                          JOGO J
                          WHERE 
                          P.IdUsuario = U.Id AND
                          P.IdJogo = J.Id AND
                          BU.IdUsuario = U.Id AND
                          B.Id = BU.IdBolao AND
                          B.IdCampeonato = C.Id AND
                          J.IdCampeonato = C.Id AND
                          J.Finalizado = 1 AND
                          P.Finalizado = 1 AND
                          BU.IdBolao = @IDBOLAO
                          GROUP by p.IdUsuario, u.Apelido
                          ORDER BY Pontos DESC, QuantidadePalpites ASC";

            var classificacao = Sql.Database.GetDbConnection().Query<ItemRankingBolaoDTO>(query, new { IDBOLAO = idBolao });
            return classificacao;
        }
    }
}
