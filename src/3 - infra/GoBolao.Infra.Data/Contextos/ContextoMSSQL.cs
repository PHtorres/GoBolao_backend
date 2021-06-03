using GoBolao.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GoBolao.Infra.Data.Contextos
{
    public class ContextoMSSQL : DbContext
    {
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
            //var servicoAppSettings = new ServiceAppSettings();
            //return servicoAppSettings.ConexaoMSSQL();

            return "server=rgbsys.dyndns.info\\rgb2014,35460;uid=sa;database=PAULO_TESTE;pwd=chicoedson;";
            //return "Server=tcp:gobolao-server.database.windows.net,1433;Initial Catalog=gobolaodb;Persist Security Info=False;User ID=ph;Password=gtfla32!@#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
