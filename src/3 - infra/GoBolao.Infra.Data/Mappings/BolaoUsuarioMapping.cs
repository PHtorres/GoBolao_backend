using GoBolao.Domain.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBolao.Infra.Data.Mappings
{
    public class BolaoUsuarioMapping : IEntityTypeConfiguration<BolaoUsuario>
    {
        public void Configure(EntityTypeBuilder<BolaoUsuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("BOLAO_USUARIO");
        }
    }
}
