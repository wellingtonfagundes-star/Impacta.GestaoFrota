using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class TipoEventoMapping : IEntityTypeConfiguration<TipoEvento>
{
    public void Configure(EntityTypeBuilder<TipoEvento> builder)
    {
        builder.HasKey(e => e.IdTipoevento).HasName("tipo_evento_pkey");

        builder.ToTable("tipo_evento", "frota");

        builder.Property(e => e.IdTipoevento)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_tipoevento");
        builder.Property(e => e.Descricao)
            .HasMaxLength(70)
            .HasColumnName("descricao");
        builder.Property(e => e.InfoAdicional).HasColumnName("info_adicional");
        builder.Property(e => e.Nome)
            .HasMaxLength(20)
            .HasColumnName("nome");
        builder.Property(e => e.Periodicidade).HasColumnName("periodicidade");
    }
}
