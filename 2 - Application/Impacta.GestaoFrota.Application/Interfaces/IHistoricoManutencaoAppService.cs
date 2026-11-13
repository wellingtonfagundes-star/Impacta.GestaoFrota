using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface IHistoricoManutencaoAppService
{
    IEnumerable<HistoricoManutencaoViewModel> ObterTodos();
    HistoricoManutencaoViewModel? ObterPorId(int id);
    IEnumerable<HistoricoManutencaoOpcaoViewModel> ObterLocais();
    IEnumerable<HistoricoManutencaoOpcaoViewModel> ObterTiposEvento();
    IEnumerable<HistoricoManutencaoOpcaoViewModel> ObterFuncionarios();
    IEnumerable<HistoricoManutencaoOpcaoViewModel> ObterClientes();
    bool Adicionar(HistoricoManutencaoViewModel viewModel);
    bool Atualizar(HistoricoManutencaoViewModel viewModel);
    bool Remover(int id);
}
