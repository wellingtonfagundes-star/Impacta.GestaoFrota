using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository;

public interface IClienteRepository : IRepositoryRead<Pessoa>, IRepositoryWrite<Pessoa>
{
    bool ExisteCpf(string cpf);

    bool ExisteCnpj(string cnpj);

    bool Remover(int id);
}
