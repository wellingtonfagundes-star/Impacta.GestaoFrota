using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class CaracteristicaMapping : IEntityTypeConfiguration<Caracteristica>
{
    public void Configure(EntityTypeBuilder<Caracteristica> builder)
    {
        builder.HasKey(e => e.IdCaracteristica).HasName("caracteristicas_pkey");

        builder.ToTable("caracteristicas", "frota");

        builder.Property(e => e.IdCaracteristica)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_caracteristica");
        builder.Property(e => e.Descricao)
            .HasMaxLength(50)
            .HasColumnName("descricao");
        builder.Property(e => e.Obs).HasColumnName("obs");
        builder.Property(e => e.Opcional).HasColumnName("opcional");
    }
}
