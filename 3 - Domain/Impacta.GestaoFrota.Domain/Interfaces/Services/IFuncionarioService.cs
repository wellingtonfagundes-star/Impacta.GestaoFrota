using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Services;

public interface IFuncionarioService
{
    Funcionario? ObterPorId(int id);

    IEnumerable<Funcionario> ObterTodos();

    bool ExisteCpf(string cpf);

    bool Adicionar(Funcionario funcionario);

    bool Atualizar(Funcionario funcionario);

    bool Remover(int id);
}
