using GoBolao.Domain.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBolao.Infra.Data.Mappings
{
    public class TimeMapping : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("time");
        }
    }
}
