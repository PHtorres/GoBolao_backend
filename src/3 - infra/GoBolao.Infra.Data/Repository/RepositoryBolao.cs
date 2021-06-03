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

        public IEnumerable<MembroBolaoDTO> ObterAdiversariosBoloes(int idMembroBolao)
        {
            var query = @"SELECT
                           U.Id IdMembro,
                           U.Apelido NomeMembro
                           FROM
                           BOLAO B,
                           BOLAO_USUARIO BU,
                           USUARIO U
                           WHERE
                           U.Id = BU.IdUsuario AND
                           B.Id = BU.IdBolao AND
                           BU.IdBolao IN (SELECT IdBolao FROM BOLAO_USUARIO BU2 WHERE BU2.IdUsuario = @IDUSUARIO)
                           GROUP BY U.Id, U.Apelido";


            var membros = Sql.Database.GetDbConnection().Query<MembroBolaoDTO>(query, new { IDUSUARIO = idMembroBolao });

            return membros;
        }

        public BolaoDTO ObterBolaoPorId(int idBolao, int idUsuario)
        {

            var query = @"SELECT 
                          B.Id IdBolao,
                          B.Nome Nome,
                          U.Apelido NomeCriador,
                          C.Nome NomeCampeonato,
                          (case when B.Privacidade = 1 then 'Privado' else 'Publico' end) Privacidade,
                          B.NomeImagemAvatar NomeImagemAvatarBolao,
                          c.NomeImagemAvatar NomeImagemAvatarCampeonato,
                          (case when @IDUSUARIO = B.IdCriador then 1 else 0 end) SouCriadorBolao,
                          (case when (SELECT COUNT(*) FROM BOLAO_USUARIO BU WHERE BU.IdUsuario = @IDUSUARIO AND BU.IdBolao = B.Id) > 0 then 1 else 0 end) PaticipoBolao,
                          (SELECT COUNT(*) FROM BOLAO_SOLICITACAO BS WHERE BS.IdBolao = B.Id AND BS.Status = 0) QuantidadeSolicitacoesAbertas
                          FROM 
                          BOLAO B,
                          USUARIO U,
                          CAMPEONATO C
                          WHERE
                          B.IdCriador = U.Id AND
                          B.IdCampeonato = C.Id AND
                          B.Id = @IDBOLAO";

            var bolaoDTO = Sql.Database.GetDbConnection().Query<BolaoDTO>(query, new { IDBOLAO = idBolao, IDUSUARIO = idUsuario }).FirstOrDefault();

            return bolaoDTO;
        }

        public IEnumerable<Bolao> ObterBoloesPeloNome(string nome)
        {
            var boloes = DbSetBolao.Where(b => b.Nome == nome);
            return boloes;
        }

        public IEnumerable<BolaoDTO> ObterBoloesPesquisa(string pesquisa, int idUsuario)
        {

            var query = @"SELECT 
                          B.Id IdBolao,
                          B.Nome Nome,
                          U.Apelido NomeCriador,
                          C.Nome NomeCampeonato,
                          (case when B.Privacidade = 1 then 'Privado' else 'Publico' end) Privacidade,
                          B.NomeImagemAvatar NomeImagemAvatarBolao,
                          c.NomeImagemAvatar NomeImagemAvatarCampeonato,
                          (case when @IDUSUARIO = B.IdCriador then 1 else 0 end) SouCriadorBolao,
                          (case when (SELECT COUNT(*) FROM BOLAO_USUARIO BU WHERE BU.IdUsuario = @IDUSUARIO AND BU.IdBolao = B.Id) > 0 then 1 else 0 end) PaticipoBolao,
                          (SELECT COUNT(*) FROM BOLAO_SOLICITACAO BS WHERE BS.IdBolao = B.Id AND BS.Status = 0) QuantidadeSolicitacoesAbertas
                          FROM 
                          BOLAO B,
                          USUARIO U,
                          CAMPEONATO C
                          WHERE
                          B.IdCriador = U.Id AND
                          B.IdCampeonato = C.Id AND
                          B.Nome LIKE '%' + @PESQUISA + '%'";

            var listaBolaoDTO = Sql.Database.GetDbConnection().Query<BolaoDTO>(query, new { PESQUISA = pesquisa, IDUSUARIO = idUsuario });

            return listaBolaoDTO;
        }

        public IEnumerable<BolaoDTO> ObterBoloesUsuario(int idUsuario)
        {
            var query = @"SELECT 
                          B.Id IdBolao,
                          B.Nome Nome,
                          U.Apelido NomeCriador,
                          C.Nome NomeCampeonato,
                          (case when B.Privacidade = 1 then 'Privado' else 'Publico' end) Privacidade,
                          B.NomeImagemAvatar NomeImagemAvatarBolao,
                          c.NomeImagemAvatar NomeImagemAvatarCampeonato,
                          (case when @IDUSUARIO = B.IdCriador then 1 else 0 end) SouCriadorBolao,
                          1 PaticipoBolao
                          FROM 
                          BOLAO B,
                          USUARIO U,
                          CAMPEONATO C,
                          BOLAO_USUARIO BU
                          WHERE
                          B.IdCriador = U.Id AND
                          B.IdCampeonato = C.Id AND
                          B.Id = BU.IdBolao AND
                          BU.IdUsuario = @IDUSUARIO";

            var listaBolaoDTO = Sql.Database.GetDbConnection().Query<BolaoDTO>(query, new { IDUSUARIO = idUsuario });

            return listaBolaoDTO;
        }

        public IEnumerable<ItemRankingBolaoDTO> ObterClassificacaoRankingBolao(int idBolao)
        {
            var query = @"SELECT 
                          U.Apelido ApelidoUsuario,
                          U.NomeImagemAvatar NomeImagemAvatarUsuario,
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
                          GROUP by p.IdUsuario, u.Apelido, u.NomeImagemAvatar
                          ORDER BY Pontos DESC, QuantidadePalpites ASC";

            var classificacao = Sql.Database.GetDbConnection().Query<ItemRankingBolaoDTO>(query, new { IDBOLAO = idBolao });
            return classificacao;
        }
    }
}
