using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository
{
    public interface IStatusFrotaRepository : IRepositoryRead<StatusFrota>, IRepositoryWrite<StatusFrota>
    {
        bool Remover(int id);
    }
}
