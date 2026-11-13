using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository;

public interface IHistoricoManutencaoRepository
{
    IEnumerable<Historico> ObterTodos();
    Historico? ObterPorId(int id);
    IEnumerable<Local> ObterLocais();
    IEnumerable<TipoEvento> ObterTiposEvento();
    IEnumerable<Funcionario> ObterFuncionarios();
    IEnumerable<Pessoa> ObterClientes();
    bool Adicionar(Historico historico);
    bool Atualizar(Historico historico);
    bool Remover(int id);
}
