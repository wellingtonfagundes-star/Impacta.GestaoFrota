using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class RetornoVeiculoMapping : IEntityTypeConfiguration<RetornoVeiculo>
{
    public void Configure(EntityTypeBuilder<RetornoVeiculo> builder)
    {
        builder.HasKey(e => e.IdRetorno).HasName("retorno_veiculo_pkey");

        builder.ToTable("retorno_veiculo", "frota");

        builder.HasIndex(e => e.IdLocalRetorno, "idx_retorno_local");

        builder.HasIndex(e => e.IdReserva, "idx_retorno_reserva");

        builder.HasIndex(e => e.IdStatus, "idx_retorno_status");

        builder.HasIndex(e => e.IdVeiculo, "idx_retorno_veiculo");

        builder.Property(e => e.IdRetorno)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_retorno");
        builder.Property(e => e.DataRetirada)
            .HasDefaultValueSql("CURRENT_DATE")
            .HasColumnName("data_retirada");
        builder.Property(e => e.DataRetorno).HasColumnName("data_retorno");
        builder.Property(e => e.IdLocalRetorno).HasColumnName("id_local_retorno");
        builder.Property(e => e.IdReserva).HasColumnName("id_reserva");
        builder.Property(e => e.IdStatus).HasColumnName("id_status");
        builder.Property(e => e.IdVeiculo).HasColumnName("id_veiculo");
        builder.Property(e => e.KmRetorno)
            .HasPrecision(10, 2)
            .HasColumnName("km_retorno");

        builder.HasOne(d => d.IdLocalRetornoNavigation).WithMany(p => p.RetornoVeiculos)
            .HasForeignKey(d => d.IdLocalRetorno)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_retorno_local");

        builder.HasOne(d => d.IdReservaNavigation).WithMany(p => p.RetornoVeiculos)
            .HasForeignKey(d => d.IdReserva)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_retorno_reserva");

        builder.HasOne(d => d.IdStatusNavigation).WithMany(p => p.RetornoVeiculos)
            .HasForeignKey(d => d.IdStatus)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_retorno_status");

        builder.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.RetornoVeiculos)
            .HasForeignKey(d => d.IdVeiculo)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_retorno_veiculo");
    }
}
