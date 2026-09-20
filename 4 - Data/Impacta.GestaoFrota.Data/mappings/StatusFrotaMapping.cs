using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class StatusFrotaMapping : IEntityTypeConfiguration<StatusFrota>
{
    public void Configure(EntityTypeBuilder<StatusFrota> builder)
    {
        builder.HasKey(e => e.IdStatusFrota).HasName("status_frota_pkey");

        builder.ToTable("status_frota", "frota");

        builder.Property(e => e.IdStatusFrota)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_status_frota");
        builder.Property(e => e.Descricao)
            .HasMaxLength(100)
            .HasColumnName("descricao");
        builder.Property(e => e.NomeStatus)
            .HasMaxLength(30)
            .HasColumnName("nome_status");
    }
}
