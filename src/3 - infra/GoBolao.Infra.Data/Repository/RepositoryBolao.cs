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

        public RepositoryBolao(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetBolao = Sql.Set<Bolao>();
            DbSetUsuario = Sql.Set<Usuario>();
            DbSetCampeonato = Sql.Set<Campeonato>();
        }

        public BolaoDTO ObterBolaoPorId(int idBolao)
        {
            var bolaoDTO = DbSetBolao.Where(bolao => bolao.Id == idBolao)
                                     .Join(DbSetUsuario, b => b.IdCriador, u => u.Id,
                                     (b, u) => new
                                     {
                                         IdCampeonato = b.IdCampeonato,
                                         Nome = b.Nome,
                                         NomeCriador = u.Apelido,
                                         NomeImagemAvatar = b.NomeImagemAvatar,
                                         Privacidade = b.Privacidade.ToString()
                                     })
                                     .Join(DbSetCampeonato, b => b.IdCampeonato, c => c.Id,
                                     (b, c) => new BolaoDTO
                                     {
                                         Nome = b.Nome,
                                         NomeCampeonato = c.Nome,
                                         NomeCriador = b.NomeCriador,
                                         NomeImagemAvatar = b.NomeImagemAvatar,
                                         Privacidade = b.Privacidade
                                     }).FirstOrDefault();

            return bolaoDTO;
        }

        public IEnumerable<BolaoDTO> ObterBoloesPesquisa(string pesquisa)
        {
            var listaBolaoDTO = DbSetBolao.Where(bolao => bolao.Nome.Contains(pesquisa))
                         .Join(DbSetUsuario, b => b.IdCriador, u => u.Id,
                         (b, u) => new
                         {
                             IdCampeonato = b.IdCampeonato,
                             Nome = b.Nome,
                             NomeCriador = u.Apelido,
                             NomeImagemAvatar = b.NomeImagemAvatar,
                             Privacidade = b.Privacidade.ToString()
                         })
                         .Join(DbSetCampeonato, b => b.IdCampeonato, c => c.Id,
                         (b, c) => new BolaoDTO
                         {
                             Nome = b.Nome,
                             NomeCampeonato = c.Nome,
                             NomeCriador = b.NomeCriador,
                             NomeImagemAvatar = b.NomeImagemAvatar,
                             Privacidade = b.Privacidade
                         }).AsEnumerable();

            return listaBolaoDTO;
        }
    }
}
