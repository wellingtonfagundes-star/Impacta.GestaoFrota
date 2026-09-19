using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository
{
    public interface IVeiculoRepository:IRepositoryRead<Veiculo>, IRepositoryWrite<Veiculo>
    {
        bool Remover(int id);
    }
}
