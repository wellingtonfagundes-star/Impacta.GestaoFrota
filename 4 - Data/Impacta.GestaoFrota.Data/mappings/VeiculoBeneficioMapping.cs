using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class VeiculoBeneficioMapping : IEntityTypeConfiguration<VeiculoBeneficio>
{
    public void Configure(EntityTypeBuilder<VeiculoBeneficio> builder)
    {
        builder.HasKey(e => e.IdBeneficio).HasName("veiculo_beneficio_pkey");

        builder.ToTable("veiculo_beneficio", "frota");

        builder.HasIndex(e => e.IdBeneficiado, "idx_beneficio_cliente");

        builder.HasIndex(e => e.IdMotorista, "idx_beneficio_motorista");

        builder.HasIndex(e => e.IdVeiculo, "idx_beneficio_veiculo");

        builder.Property(e => e.IdBeneficio)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_beneficio");
        builder.Property(e => e.DataDevolucao).HasColumnName("data_devolucao");
        builder.Property(e => e.DataEntrega).HasColumnName("data_entrega");
        builder.Property(e => e.IdBeneficiado).HasColumnName("id_beneficiado");
        builder.Property(e => e.IdMotorista).HasColumnName("id_motorista");
        builder.Property(e => e.IdVeiculo).HasColumnName("id_veiculo");
        builder.Property(e => e.LocalVaga)
            .HasMaxLength(100)
            .HasColumnName("local_vaga");
        builder.Property(e => e.UsoFds).HasColumnName("uso_fds");

        builder.HasOne(d => d.IdBeneficiadoNavigation).WithMany(p => p.VeiculoBeneficioIdBeneficiadoNavigations)
            .HasForeignKey(d => d.IdBeneficiado)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_beneficio_pessoa");

        builder.HasOne(d => d.IdMotoristaNavigation).WithMany(p => p.VeiculoBeneficioIdMotoristaNavigations)
            .HasForeignKey(d => d.IdMotorista)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_beneficio_motorista");

        builder.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.VeiculoBeneficios)
            .HasForeignKey(d => d.IdVeiculo)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_beneficio_veiculo");
    }
}
