using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class ReservaVeiculoMapping : IEntityTypeConfiguration<ReservaVeiculo>
{
    public void Configure(EntityTypeBuilder<ReservaVeiculo> builder)
    {
        builder.HasKey(e => e.IdReserva).HasName("reserva_veiculo_pkey");

        builder.ToTable("reserva_veiculo", "frota");

        builder.HasIndex(e => e.IdClienteSolicitante, "idx_reserva_cliente");

        builder.HasIndex(e => e.IdFuncionarioAprovador, "idx_reserva_funcionario");

        builder.HasIndex(e => e.IdLocalDestino, "idx_reserva_local");

        builder.Property(e => e.IdReserva)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_reserva");
        builder.Property(e => e.Aprovado).HasColumnName("aprovado");
        builder.Property(e => e.Capacete).HasColumnName("capacete");
        builder.Property(e => e.Data).HasColumnName("data");
        builder.Property(e => e.Emergencia).HasColumnName("emergencia");
        builder.Property(e => e.EqpSeguranca).HasColumnName("eqp_seguranca");
        builder.Property(e => e.Estacionamento).HasColumnName("estacionamento");
        builder.Property(e => e.Gps).HasColumnName("gps");
        builder.Property(e => e.IdClienteSolicitante).HasColumnName("id_cliente_solicitante");
        builder.Property(e => e.IdFuncionarioAprovador).HasColumnName("id_funcionario_aprovador");
        builder.Property(e => e.IdLocalDestino).HasColumnName("id_local_destino");
        builder.Property(e => e.KmEstimada)
            .HasPrecision(10, 2)
            .HasColumnName("km_estimada");
        builder.Property(e => e.Motivo).HasColumnName("motivo");
        builder.Property(e => e.ObsGeral).HasColumnName("obs_geral");
        builder.Property(e => e.Preferencia).HasColumnName("preferencia");

        builder.HasOne(d => d.IdClienteSolicitanteNavigation).WithMany(p => p.ReservaVeiculos)
            .HasForeignKey(d => d.IdClienteSolicitante)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_reserva_cliente");

        builder.HasOne(d => d.IdFuncionarioAprovadorNavigation).WithMany(p => p.ReservaVeiculos)
            .HasForeignKey(d => d.IdFuncionarioAprovador)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_reserva_funcionario");

        builder.HasOne(d => d.IdLocalDestinoNavigation).WithMany(p => p.ReservaVeiculos)
            .HasForeignKey(d => d.IdLocalDestino)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_reserva_local");
    }
}
