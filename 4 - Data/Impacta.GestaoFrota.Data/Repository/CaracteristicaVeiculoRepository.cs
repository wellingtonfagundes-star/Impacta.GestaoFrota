using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.Data.Repository
{
    public class CaracteristicaVeiculoRepository : Repository<CaracteristicaVeiculo>, ICaracteristicaVeiculoRepository
    {
        public CaracteristicaVeiculoRepository(FrotaContext context)
            : base(context)
        {
        }

        public override IEnumerable<CaracteristicaVeiculo> ObterTodos()
        {
            return DbSet
                .Include(cv => cv.IdVeiculoNavigation)
                .Include(cv => cv.IdCaracteristicaNavigation)
                .AsNoTracking()
                .ToList();
        }

        public CaracteristicaVeiculo ObterPorIds(int idVeiculo, int idCaracteristica)
        {
            return DbSet
                .Include(cv => cv.IdVeiculoNavigation)
                .Include(cv => cv.IdCaracteristicaNavigation)
                .FirstOrDefault(cv => cv.IdVeiculo == idVeiculo && cv.IdCaracteristica == idCaracteristica);
        }

        public bool Remover(int idVeiculo, int idCaracteristica)
        {
            try
            {
                var caracteristicaVeiculo = DbSet.Find(idVeiculo, idCaracteristica);
                if (caracteristicaVeiculo == null)
                    return false;

                DbSet.Remove(caracteristicaVeiculo);
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
