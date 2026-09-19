using System.Linq.Expressions;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;

    public VeiculoService(IVeiculoRepository veiculoRepository)
    {
        _veiculoRepository = veiculoRepository;
    }

    public Veiculo ObterPorId(Guid id)
    {
        return _veiculoRepository.ObterPorId(id);
    }

    public Veiculo ObterPorId(int id)
    {
        return _veiculoRepository.ObterPorId(id);
    }

    public IEnumerable<Veiculo> ObterTodos()
    {
        return _veiculoRepository.ObterTodos();
    }

    public IEnumerable<Veiculo> ObterTodosPaginado(int size, int reg)
    {
        return _veiculoRepository.ObterTodosPaginado(size, reg);
    }

    public IEnumerable<Veiculo> Buscar(Expression<Func<Veiculo, bool>> predicate)
    {
        return _veiculoRepository.Buscar(predicate);
    }

    public bool Adicionar(Veiculo obj)
    {
        return _veiculoRepository.Adicionar(obj);
    }

    public bool Atualizar(Veiculo obj)
    {
        return _veiculoRepository.Atualizar(obj);
    }

    public bool Remover(Guid id)
    {
        return _veiculoRepository.Remover(id);
    }

    public bool Remover(int id)
    {
        return _veiculoRepository.Remover(id);
    }

    public int SaveChanges()
    {
        return _veiculoRepository.SaveChanges();
    }

    public void Dispose()
    {
        _veiculoRepository.Dispose();
    }
}
