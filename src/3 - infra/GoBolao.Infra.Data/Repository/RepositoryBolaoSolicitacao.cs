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
    public class RepositoryBolaoSolicitacao : RepositoryGenerico<BolaoSolicitacao>, IRepositoryBolaoSolicitacao
    {
        private ContextoMSSQL Sql;
        private DbSet<BolaoSolicitacao> DbSetBolaoSolicitacao;
        private DbSet<Usuario> DbSetUsuario;
        private DbSet<Bolao> DbSetBolao;

        public RepositoryBolaoSolicitacao(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetBolaoSolicitacao = Sql.Set<BolaoSolicitacao>();
            DbSetUsuario = Sql.Set<Usuario>();
            DbSetBolao = Sql.Set<Bolao>();
        }

        public IEnumerable<BolaoSolicitacaoDTO> ObterSolicitacoesPorBolao(int idBolao)
        {
            var solicitacoes = DbSetBolaoSolicitacao.Where(s => s.IdBolao == idBolao)
                                                    .OrderBy(s => s.Status)
                                                    .Join(DbSetUsuario, s => s.IdUsuarioSolicitante, u => u.Id, (s, u) =>
                                                    new
                                                    {
                                                        ApelidoUsuarioSolicitante = u.Apelido,
                                                        IdBolao = s.IdBolao,
                                                        IdSolicitacao = s.Id,
                                                        Status = s.Status.ToString()
                                                    })
                                                    .Join(DbSetBolao, x => x.IdBolao, b => b.Id, (x, b) =>
                                                    new BolaoSolicitacaoDTO
                                                    {
                                                        ApelidoUsuarioSolicitante = x.ApelidoUsuarioSolicitante,
                                                        IdBolao = x.IdBolao,
                                                        IdSolicitacao = x.IdSolicitacao,
                                                        NomeBolao = b.Nome,
                                                        Status = x.Status
                                                    });

            return solicitacoes;
        }
    }
}
