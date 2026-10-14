using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class TipoEventoService : ITipoEventoService
{
    private readonly ITipoEventoRepository _repository;

    public TipoEventoService(ITipoEventoRepository repository)
    {
        _repository = repository;
    }

    public TipoEvento? ObterPorId(int id)
    {
        return _repository.ObterPorId(id);
    }

    public IEnumerable<TipoEvento> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public void Adicionar(TipoEvento tipoEvento)
    {
        ArgumentNullException.ThrowIfNull(tipoEvento);
        _repository.Adicionar(tipoEvento);
    }

    public void Atualizar(TipoEvento tipoEvento)
    {
        ArgumentNullException.ThrowIfNull(tipoEvento);
        _repository.Atualizar(tipoEvento);
    }

    public bool Remover(int id)
    {
        return _repository.Remover(id);
    }

    public void Salvar()
    {
        _repository.SaveChanges();
    }
}
