using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services;

public interface ICaracteristicaService : IRepositoryRead<Caracteristica>, IRepositoryWrite<Caracteristica>
{
    bool Remover(int id);
}
