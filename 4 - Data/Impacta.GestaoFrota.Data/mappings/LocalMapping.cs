using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class LocalMapping : IEntityTypeConfiguration<Locai>
{
    public void Configure(EntityTypeBuilder<Locai> builder)
    {
        builder.HasKey(e => e.IdLocal).HasName("locais_pkey");

        builder.ToTable("locais", "frota");

        builder.Property(e => e.IdLocal)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_local");
        builder.Property(e => e.DescrServico).HasColumnName("descr_servico");
        builder.Property(e => e.Endereco)
            .HasMaxLength(150)
            .HasColumnName("endereco");
        builder.Property(e => e.Nome)
            .HasMaxLength(70)
            .HasColumnName("nome");
        builder.Property(e => e.NomeContato)
            .HasMaxLength(37)
            .HasColumnName("nome_contato");
        builder.Property(e => e.Telefone)
            .HasMaxLength(15)
            .HasColumnName("telefone");
    }
}
