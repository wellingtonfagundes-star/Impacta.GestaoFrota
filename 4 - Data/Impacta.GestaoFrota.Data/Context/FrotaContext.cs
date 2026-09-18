using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Data.Context;

public class FrotaContext : DbContext
{
    public FrotaContext(DbContextOptions<FrotaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Caracteristica> Caracteristicas { get; set; }
    public virtual DbSet<CaracteristicaVeiculo> CaracteristicaVeiculos { get; set; }
    public virtual DbSet<Funcionario> Funcionarios { get; set; }
    public virtual DbSet<Historico> Historicos { get; set; }
    public virtual DbSet<Locai> Locais { get; set; }
    public virtual DbSet<Pessoa> Pessoas { get; set; }
    public virtual DbSet<ReservaVeiculo> ReservaVeiculos { get; set; }
    public virtual DbSet<RetornoVeiculo> RetornoVeiculos { get; set; }
    public virtual DbSet<StatusFrotum> StatusFrota { get; set; }
    public virtual DbSet<StatusVeiculo> StatusVeiculos { get; set; }
    public virtual DbSet<TipoEvento> TipoEventos { get; set; }
    public virtual DbSet<Veiculo> Veiculos { get; set; }
    public virtual DbSet<VeiculoBeneficio> VeiculoBeneficios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
