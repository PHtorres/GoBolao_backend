using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Repository;
using GoBolao.Infra.Data.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Infra.Data.Repository
{
    public class RepositoryGenerico<EntidadeGenerica> : IRepositoryGenerico<EntidadeGenerica> where EntidadeGenerica : EntidadeBase
    {
        protected ContextoMSSQL Sql;
        protected DbSet<EntidadeGenerica> DbSetGenerico;

        public RepositoryGenerico(ContextoMSSQL _sql)
        {
            Sql = _sql;
            DbSetGenerico = Sql.Set<EntidadeGenerica>();
        }

        public void Adicionar(EntidadeGenerica obj)
        {
            DbSetGenerico.Add(obj);
        }

        public void Atualizar(EntidadeGenerica obj)
        {
            DbSetGenerico.Update(obj);
        }

        public void Dispose()
        {
            Sql.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<EntidadeGenerica> Listar()
        {
            return DbSetGenerico.AsEnumerable();
        }

        public EntidadeGenerica Obter(int id)
        {
            return DbSetGenerico.Find(id);
        }

        public void Remover(EntidadeGenerica obj)
        {
            DbSetGenerico.Remove(obj);
        }

        public void Salvar()
        {
            Sql.SaveChanges();
        }
    }
}
