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

        public RepositoryBolaoSolicitacao(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetBolaoSolicitacao = Sql.Set<BolaoSolicitacao>();
        }
    }
}
