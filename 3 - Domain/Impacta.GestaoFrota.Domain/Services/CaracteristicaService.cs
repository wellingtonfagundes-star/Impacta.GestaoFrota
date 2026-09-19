using System.Linq.Expressions;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class CaracteristicaService : ICaracteristicaService
{
    private readonly ICaracteristicaRepository _caracteristicaRepository;

    public CaracteristicaService(ICaracteristicaRepository caracteristicaRepository)
    {
        _caracteristicaRepository = caracteristicaRepository;
    }

    public Caracteristica ObterPorId(Guid id)
    {
        return _caracteristicaRepository.ObterPorId(id);
    }

    public Caracteristica ObterPorId(int id)
    {
        return _caracteristicaRepository.ObterPorId(id);
    }

    public IEnumerable<Caracteristica> ObterTodos()
    {
        return _caracteristicaRepository.ObterTodos();
    }

    public IEnumerable<Caracteristica> ObterTodosPaginado(int size, int reg)
    {
        return _caracteristicaRepository.ObterTodosPaginado(size, reg);
    }

    public IEnumerable<Caracteristica> Buscar(Expression<Func<Caracteristica, bool>> predicate)
    {
        return _caracteristicaRepository.Buscar(predicate);
    }

    public bool Adicionar(Caracteristica obj)
    {
        return _caracteristicaRepository.Adicionar(obj);
    }

    public bool Atualizar(Caracteristica obj)
    {
        return _caracteristicaRepository.Atualizar(obj);
    }

    public bool Remover(Guid id)
    {
        return _caracteristicaRepository.Remover(id);
    }

    public bool Remover(int id)
    {
        return _caracteristicaRepository.Remover(id);
    }

    public int SaveChanges()
    {
        return _caracteristicaRepository.SaveChanges();
    }

    public void Dispose()
    {
        _caracteristicaRepository.Dispose();
    }
}
