using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Usuarios.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBolao.Infra.Data.Mappings
{
    public class PalpiteMapping : IEntityTypeConfiguration<Palpite>
    {
        public void Configure(EntityTypeBuilder<Palpite> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("palpite");
        }
    }
}
