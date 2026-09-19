using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository
{
    public interface ICaracteristicaRepository : IRepositoryRead<Caracteristica>, IRepositoryWrite<Caracteristica>
    {
        bool Remover(int id);
    }
}
