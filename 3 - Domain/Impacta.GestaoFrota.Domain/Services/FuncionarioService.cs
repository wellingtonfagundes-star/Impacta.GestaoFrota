using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _repository;

    public FuncionarioService(IFuncionarioRepository repository)
    {
        _repository = repository;
    }

    public Funcionario? ObterPorId(int id)
    {
        return _repository.ObterPorId(id);
    }

    public IEnumerable<Funcionario> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public bool Adicionar(Funcionario funcionario)
    {
        ArgumentNullException.ThrowIfNull(funcionario);
        return _repository.Adicionar(funcionario);
    }

    public bool Atualizar(Funcionario funcionario)
    {
        ArgumentNullException.ThrowIfNull(funcionario);
        return _repository.Atualizar(funcionario);
    }

    public bool Remover(int id)
    {
        return _repository.Remover(id);
    }
}
