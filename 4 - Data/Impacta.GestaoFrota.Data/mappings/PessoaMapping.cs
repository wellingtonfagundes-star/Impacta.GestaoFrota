using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasKey(e => e.IdPessoa).HasName("pessoa_pkey");

        builder.ToTable("pessoa", "frota");

        builder.HasIndex(e => e.Cnpj, "uk_pessoa_cnpj").IsUnique();

        builder.HasIndex(e => e.Cpf, "uk_pessoa_cpf").IsUnique();

        builder.Property(e => e.IdPessoa)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_pessoa");
        builder.Property(e => e.Bairro)
            .HasMaxLength(70)
            .HasColumnName("bairro");
        builder.Property(e => e.Cep)
            .HasMaxLength(15)
            .HasColumnName("cep");
        builder.Property(e => e.Cidade)
            .HasMaxLength(70)
            .HasColumnName("cidade");
        builder.Property(e => e.Cnpj)
            .HasMaxLength(15)
            .HasColumnName("cnpj");
        builder.Property(e => e.Cpf)
            .HasMaxLength(11)
            .HasColumnName("cpf");
        builder.Property(e => e.DataCriacao)
            .HasDefaultValueSql("CURRENT_DATE")
            .HasColumnName("data_criacao");
        builder.Property(e => e.DataUltAlteracao).HasColumnName("data_ult_alteracao");
        builder.Property(e => e.Endereco)
            .HasMaxLength(150)
            .HasColumnName("endereco");
        builder.Property(e => e.Estado)
            .HasMaxLength(70)
            .HasColumnName("estado");
        builder.Property(e => e.Nome)
            .HasMaxLength(70)
            .HasColumnName("nome");
        builder.Property(e => e.Sexo)
            .HasMaxLength(1)
            .HasColumnName("sexo");
        builder.Property(e => e.TipoCliente)
            .HasMaxLength(2)
            .IsFixedLength()
            .HasColumnName("tipo_cliente");
    }
}
