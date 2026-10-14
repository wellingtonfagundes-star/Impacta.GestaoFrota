using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository;

public interface ILocalAssistenciaRepository : IRepositoryRead<Local>, IRepositoryWrite<Local>
{
    bool Remover(int id);
}
