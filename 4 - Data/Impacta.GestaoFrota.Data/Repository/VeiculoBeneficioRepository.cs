using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository;

public class VeiculoBeneficioRepository : Repository<VeiculoBeneficio>, IVeiculoBeneficioRepository
{
    public VeiculoBeneficioRepository(FrotaContext context)
        : base(context)
    {
    }

    public IEnumerable<VeiculoBeneficio> ObterTodosComRelacionamentos()
    {
        return DbSet.AsNoTracking()
            .Include(item => item.IdBeneficiadoNavigation)
            .Include(item => item.IdMotoristaNavigation)
            .Include(item => item.IdVeiculoNavigation)
            .OrderByDescending(item => item.IdBeneficio)
            .ToList();
    }

    public VeiculoBeneficio? ObterPorIdComRelacionamentos(int id)
    {
        return DbSet
            .Include(item => item.IdBeneficiadoNavigation)
            .Include(item => item.IdMotoristaNavigation)
            .Include(item => item.IdVeiculoNavigation)
            .SingleOrDefault(item => item.IdBeneficio == id);
    }

    public IEnumerable<Pessoa> ObterPessoas()
    {
        return db.Pessoas.AsNoTracking()
            .OrderBy(pessoa => pessoa.Nome)
            .ToList();
    }

    public IEnumerable<Veiculo> ObterVeiculos()
    {
        return db.Veiculos.AsNoTracking()
            .OrderBy(veiculo => veiculo.Placa)
            .ToList();
    }

    public bool RemoverPorId(int id)
    {
        var beneficio = DbSet.Find(id);
        if (beneficio is null)
            return false;

        DbSet.Remove(beneficio);
        return SaveChanges() > 0;
    }
}