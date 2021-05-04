using GoBolao.Domain.Usuarios.Entidades;
using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Infra.Data.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Infra.Data.Repository
{
    public class RepositoryUsuario : RepositoryGenerico<Usuario>, IRepositoryUsuario
    {
        private ContextoMSSQL Sql;
        private DbSet<Usuario> DbSetUsuario;

        public RepositoryUsuario(ContextoMSSQL _sql) : base(_sql)
        {
            Sql = _sql;
            DbSetUsuario = Sql.Set<Usuario>();
        }

        public Usuario ObterUsuarioPorEmail(string email)
        {
            var usuario = DbSetUsuario.Where(item => item.Email == email).FirstOrDefault();
            return usuario;
        }
    }
}
