using GoBolao.Infra.Data.Interfaces;
using GoBolao.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GoBolao.Infra.Data.Contextos
{
    public class ContextoMSSQL : DbContext
    {
        private readonly IServiceAppSettings ServicoAppSettings;

        public ContextoMSSQL(IServiceAppSettings servicoAppSettings)
        {
            ServicoAppSettings = servicoAppSettings;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsuarioMapping());
            builder.ApplyConfiguration(new CampeonatoMapping());
            builder.ApplyConfiguration(new BolaoMapping());
            builder.ApplyConfiguration(new JogoMapping());
            builder.ApplyConfiguration(new PalpiteMapping());
            builder.ApplyConfiguration(new TimeMapping());
            builder.ApplyConfiguration(new BolaoUsuarioMapping());
            builder.ApplyConfiguration(new BolaoSolicitacaoMapping());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ObterConexaoSQL());
        }

        private string ObterConexaoSQL()
        {
            return ServicoAppSettings.ConexaoMSSQL();
        }
    }
}
