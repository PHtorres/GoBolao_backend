using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Usuarios.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBolao.Infra.Data.Mappings
{
    public class JogoMapping : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("jogo");
        }
    }
}
