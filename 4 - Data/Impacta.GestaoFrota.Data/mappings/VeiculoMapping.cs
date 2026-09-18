using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.HasKey(e => e.IdVeiculo).HasName("veiculos_pkey");

        builder.ToTable("veiculos", "frota");

        builder.HasIndex(e => e.IdStatusFrota, "idx_veiculo_status_frota");

        builder.HasIndex(e => e.Chassi, "uk_veiculo_chassi").IsUnique();

        builder.HasIndex(e => e.NrAtivo, "uk_veiculo_nr_ativo").IsUnique();

        builder.HasIndex(e => e.NrIdentificacao, "uk_veiculo_nr_identificacao").IsUnique();

        builder.HasIndex(e => e.Placa, "uk_veiculo_placa").IsUnique();

        builder.HasIndex(e => e.Renavam, "uk_veiculo_renavam").IsUnique();

        builder.Property(e => e.IdVeiculo)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_veiculo");
        builder.Property(e => e.AnoModelo).HasColumnName("ano_modelo");
        builder.Property(e => e.Chassi)
            .HasMaxLength(25)
            .HasColumnName("chassi");
        builder.Property(e => e.Cor)
            .HasMaxLength(30)
            .HasColumnName("cor");
        builder.Property(e => e.DataCompra).HasColumnName("data_compra");
        builder.Property(e => e.Fabricante)
            .HasMaxLength(70)
            .HasColumnName("fabricante");
        builder.Property(e => e.IdStatusFrota).HasColumnName("id_status_frota");
        builder.Property(e => e.InfoRodizio).HasColumnName("info_rodizio");
        builder.Property(e => e.KmAtual).HasColumnName("km_atual");
        builder.Property(e => e.NrApoliceSeguro).HasColumnName("nr_apolice_seguro");
        builder.Property(e => e.NrAtivo).HasColumnName("nr_ativo");
        builder.Property(e => e.NrIdentificacao)
            .HasMaxLength(20)
            .HasColumnName("nr_identificacao");
        builder.Property(e => e.Placa)
            .HasMaxLength(10)
            .HasColumnName("placa");
        builder.Property(e => e.Renavam)
            .HasMaxLength(15)
            .HasColumnName("renavam");
        builder.Property(e => e.Seguradora)
            .HasMaxLength(30)
            .HasColumnName("seguradora");
        builder.Property(e => e.TipoHabilitacao)
            .HasMaxLength(12)
            .HasColumnName("tipo_habilitacao");
        builder.Property(e => e.TipoVeiculo)
            .HasMaxLength(3)
            .HasColumnName("tipo_veiculo");

        builder.HasOne(d => d.IdStatusFrotaNavigation).WithMany(p => p.Veiculos)
            .HasForeignKey(d => d.IdStatusFrota)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_veiculo_status_frota");
    }
}
