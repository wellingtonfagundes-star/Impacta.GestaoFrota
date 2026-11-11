using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class ReservaVeiculoService : IReservaVeiculoService
{
    private readonly IReservaVeiculoRepository _repository;

    public ReservaVeiculoService(IReservaVeiculoRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<ReservaVeiculo> ObterTodosComRelacionamentos() => _repository.ObterTodosComRelacionamentos();
    public ReservaVeiculo? ObterPorIdComRelacionamentos(int id) => _repository.ObterPorIdComRelacionamentos(id);
    public IEnumerable<Veiculo> ObterVeiculos() => _repository.ObterVeiculos();
    public IEnumerable<Local> ObterLocais() => _repository.ObterLocais();
    public IEnumerable<Pessoa> ObterPessoas() => _repository.ObterPessoas();
    public IEnumerable<Funcionario> ObterFuncionarios() => _repository.ObterFuncionarios();

    public bool Salvar(ReservaVeiculo reserva, RetornoVeiculo retorno)
    {
        ArgumentNullException.ThrowIfNull(reserva);
        ArgumentNullException.ThrowIfNull(retorno);
        return _repository.Salvar(reserva, retorno);
    }

    public bool Remover(int id) => _repository.Remover(id);
}
