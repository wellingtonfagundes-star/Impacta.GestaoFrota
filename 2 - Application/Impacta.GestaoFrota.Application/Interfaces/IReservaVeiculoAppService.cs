using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface IReservaVeiculoAppService
{
    IEnumerable<ReservaVeiculoViewModel> ObterTodos();
    ReservaVeiculoViewModel? ObterPorId(int id);
    IEnumerable<ReservaVeiculoOpcaoViewModel> ObterVeiculos();
    IEnumerable<ReservaVeiculoOpcaoViewModel> ObterLocais();
    IEnumerable<ReservaVeiculoOpcaoViewModel> ObterPessoas();
    IEnumerable<ReservaVeiculoOpcaoViewModel> ObterFuncionarios();
    bool Adicionar(ReservaVeiculoViewModel viewModel);
    bool Atualizar(ReservaVeiculoViewModel viewModel);
    bool Remover(int id);
}
