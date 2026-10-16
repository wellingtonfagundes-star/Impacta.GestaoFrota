using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository;

public interface IClienteRepository : IRepositoryRead<Pessoa>, IRepositoryWrite<Pessoa>
{
    bool Remover(int id);
}
