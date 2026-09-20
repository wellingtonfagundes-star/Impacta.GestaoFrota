using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository
{
    public interface ICaracteristicaVeiculoRepository : IRepositoryRead<CaracteristicaVeiculo>, IRepositoryWrite<CaracteristicaVeiculo>
    {
        bool Remover(int idVeiculo, int idCaracteristica);
        CaracteristicaVeiculo ObterPorIds(int idVeiculo, int idCaracteristica);
    }
}
