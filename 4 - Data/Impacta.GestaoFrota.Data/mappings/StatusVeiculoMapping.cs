using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class StatusVeiculoMapping : IEntityTypeConfiguration<StatusVeiculo>
{
    public void Configure(EntityTypeBuilder<StatusVeiculo> builder)
    {
        builder.HasKey(e => e.IdStatus).HasName("status_veiculo_pkey");

        builder.ToTable("status_veiculo", "frota");

        builder.Property(e => e.IdStatus)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_status");
        builder.Property(e => e.Combustivel)
            .HasMaxLength(20)
            .HasColumnName("combustivel");
        builder.Property(e => e.KmEntregue).HasColumnName("km_entregue");
        builder.Property(e => e.Lataria)
            .HasMaxLength(100)
            .HasColumnName("lataria");
        builder.Property(e => e.ObsGeral).HasColumnName("obs_geral");
        builder.Property(e => e.Pneus)
            .HasMaxLength(50)
            .HasColumnName("pneus");
    }
}
