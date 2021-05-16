using GoBolao.Domain.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBolao.Infra.Data.Mappings
{
    public class BolaoMapping : IEntityTypeConfiguration<Bolao>
    {
        public void Configure(EntityTypeBuilder<Bolao> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("bolao");
        }
    }
}
