using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services;

public interface IClienteService
{
    Pessoa? ObterPorId(int id);

    IEnumerable<Pessoa> ObterTodos();

    bool Adicionar(Pessoa pessoa);

    bool Atualizar(Pessoa pessoa);

    bool Remover(int id);
}
