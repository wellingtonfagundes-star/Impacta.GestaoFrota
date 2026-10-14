using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository;

public interface IFuncionarioRepository : IRepositoryRead<Funcionario>, IRepositoryWrite<Funcionario>
{
    bool Remover(int id);
}
