using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class LocalAssistenciaService : ILocalAssistenciaService
{
    private readonly ILocalAssistenciaRepository _repository;

    public LocalAssistenciaService(ILocalAssistenciaRepository repository)
    {
        _repository = repository;
    }

    public Local ObterPorId(int id)
    {
        return _repository.ObterPorId(id);
    }

    public IEnumerable<Local> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public bool Adicionar(Local local)
    {
        ArgumentNullException.ThrowIfNull(local);
        return _repository.Adicionar(local);
    }

    public bool Atualizar(Local local)
    {
        ArgumentNullException.ThrowIfNull(local);
        return _repository.Atualizar(local);
    }

    public bool Remover(int id)
    {
        return _repository.Remover(id);
    }

}
