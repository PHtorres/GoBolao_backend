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
    public class RepositoryTime : RepositoryGenerico<Time>, IRepositoryTime
    {
        private ContextoMSSQL Sql;
        private DbSet<Time> DbSetTime;

        public RepositoryTime(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetTime = Sql.Set<Time>();
        }
    }
}
