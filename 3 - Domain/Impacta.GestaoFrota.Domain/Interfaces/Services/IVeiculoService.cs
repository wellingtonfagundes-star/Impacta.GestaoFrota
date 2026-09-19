using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services;

public interface IVeiculoService : IRepositoryRead<Veiculo>, IRepositoryWrite<Veiculo>
{
    bool Remover(int id);
}
