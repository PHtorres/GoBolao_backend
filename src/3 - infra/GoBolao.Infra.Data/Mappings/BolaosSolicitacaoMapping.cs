using GoBolao.Domain.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBolao.Infra.Data.Mappings
{
    public class BolaoSolicitacaoMapping : IEntityTypeConfiguration<BolaoSolicitacao>
    {
        public void Configure(EntityTypeBuilder<BolaoSolicitacao> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("BOLAO_SOLICITACAO");
        }
    }
}
