using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class CaracteristicaVeiculoMapping : IEntityTypeConfiguration<CaracteristicaVeiculo>
{
    public void Configure(EntityTypeBuilder<CaracteristicaVeiculo> builder)
    {
        builder.HasKey(e => new { e.IdVeiculo, e.IdCaracteristica }).HasName("caracteristica_veiculo_pkey");

        builder.ToTable("caracteristica_veiculo", "frota");

        builder.HasIndex(e => e.IdCaracteristica, "idx_cv_caracteristica");

        builder.HasIndex(e => e.IdVeiculo, "idx_cv_veiculo");

        builder.Property(e => e.IdVeiculo).HasColumnName("id_veiculo");
        builder.Property(e => e.IdCaracteristica).HasColumnName("id_caracteristica");
        builder.Property(e => e.Comentario).HasColumnName("comentario");
        builder.Property(e => e.DataCriacao)
            .HasDefaultValueSql("CURRENT_DATE")
            .HasColumnName("data_criacao");
        builder.Property(e => e.DataUltAlteracao).HasColumnName("data_ult_alteracao");
        builder.Property(e => e.Valor)
            .HasPrecision(18, 2)
            .HasColumnName("valor");

        builder.HasOne(d => d.IdCaracteristicaNavigation).WithMany(p => p.CaracteristicaVeiculos)
            .HasForeignKey(d => d.IdCaracteristica)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_cv_caracteristica");

        builder.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.CaracteristicaVeiculos)
            .HasForeignKey(d => d.IdVeiculo)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_cv_veiculo");
    }
}
