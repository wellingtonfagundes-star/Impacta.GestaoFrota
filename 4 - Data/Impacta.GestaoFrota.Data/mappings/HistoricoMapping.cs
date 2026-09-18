using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class HistoricoMapping : IEntityTypeConfiguration<Historico>
{
    public void Configure(EntityTypeBuilder<Historico> builder)
    {
        builder.HasKey(e => e.IdHistorico).HasName("historico_pkey");

        builder.ToTable("historico", "frota");

        builder.HasIndex(e => e.IdClienteAtendido, "idx_hist_cliente");

        builder.HasIndex(e => e.IdTipoevento, "idx_hist_evento");

        builder.HasIndex(e => e.IdFuncionarioResponsavel, "idx_hist_funcionario");

        builder.HasIndex(e => e.IdLocal, "idx_hist_local");

        builder.Property(e => e.IdHistorico)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_historico");
        builder.Property(e => e.DataEvento)
            .HasDefaultValueSql("CURRENT_DATE")
            .HasColumnName("data_evento");
        builder.Property(e => e.DescrEvento)
            .HasMaxLength(50)
            .HasColumnName("descr_evento");
        builder.Property(e => e.IdClienteAtendido).HasColumnName("id_cliente_atendido");
        builder.Property(e => e.IdFuncionarioResponsavel).HasColumnName("id_funcionario_responsavel");
        builder.Property(e => e.IdLocal).HasColumnName("id_local");
        builder.Property(e => e.IdTipoevento).HasColumnName("id_tipoevento");
        builder.Property(e => e.NrNotaFiscal).HasColumnName("nr_nota_fiscal");
        builder.Property(e => e.Obs).HasColumnName("obs");
        builder.Property(e => e.Quilometragem).HasColumnName("quilometragem");
        builder.Property(e => e.Valor)
            .HasPrecision(10, 2)
            .HasColumnName("valor");

        builder.HasOne(d => d.IdClienteAtendidoNavigation).WithMany(p => p.Historicos)
            .HasForeignKey(d => d.IdClienteAtendido)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_hist_cliente");

        builder.HasOne(d => d.IdFuncionarioResponsavelNavigation).WithMany(p => p.Historicos)
            .HasForeignKey(d => d.IdFuncionarioResponsavel)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_hist_funcionario");

        builder.HasOne(d => d.IdLocalNavigation).WithMany(p => p.Historicos)
            .HasForeignKey(d => d.IdLocal)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_hist_local");

        builder.HasOne(d => d.IdTipoeventoNavigation).WithMany(p => p.Historicos)
            .HasForeignKey(d => d.IdTipoevento)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_hist_evento");
    }
}
