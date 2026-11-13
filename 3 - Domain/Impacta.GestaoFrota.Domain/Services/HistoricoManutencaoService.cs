using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class HistoricoManutencaoService : IHistoricoManutencaoService
{
    private readonly IHistoricoManutencaoRepository _repository;

    public HistoricoManutencaoService(IHistoricoManutencaoRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Historico> ObterTodos() => _repository.ObterTodos();

    public Historico? ObterPorId(int id) => _repository.ObterPorId(id);

    public IEnumerable<Local> ObterLocais() => _repository.ObterLocais();

    public IEnumerable<TipoEvento> ObterTiposEvento() => _repository.ObterTiposEvento();

    public IEnumerable<Funcionario> ObterFuncionarios() => _repository.ObterFuncionarios();

    public IEnumerable<Pessoa> ObterClientes() => _repository.ObterClientes();

    public bool Adicionar(Historico historico) => _repository.Adicionar(historico);

    public bool Atualizar(Historico historico) => _repository.Atualizar(historico);

    public bool Remover(int id) => _repository.Remover(id);
}
