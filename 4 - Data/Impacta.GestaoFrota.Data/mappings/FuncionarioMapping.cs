using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.mappings;

public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.HasKey(e => e.IdFuncionario).HasName("funcionario_pkey");

        builder.ToTable("funcionario", "frota");

        builder.HasIndex(e => e.Cpf, "uk_funcionario_cpf").IsUnique();

        builder.HasIndex(e => e.Matricula, "uk_funcionario_matricula").IsUnique();

        builder.Property(e => e.IdFuncionario)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_funcionario");
        builder.Property(e => e.Bairro)
            .HasMaxLength(70)
            .HasColumnName("bairro");
        builder.Property(e => e.Cep)
            .HasMaxLength(15)
            .HasColumnName("cep");
        builder.Property(e => e.Cidade)
            .HasMaxLength(70)
            .HasColumnName("cidade");
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
        builder.Property(e => e.Matricula)
            .HasMaxLength(30)
            .HasColumnName("matricula");
        builder.Property(e => e.Nome)
            .HasMaxLength(70)
            .HasColumnName("nome");
        builder.Property(e => e.Sexo)
            .HasMaxLength(1)
            .HasColumnName("sexo");
    }
}
