using GoBolao.Domain.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBolao.Infra.Data.Mappings
{
    public class CampeonatoMapping : IEntityTypeConfiguration<Campeonato>
    {
        public void Configure(EntityTypeBuilder<Campeonato> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("campeonato");
        }
    }
}
