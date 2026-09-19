using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface IVeiculoAppService
{
    VeiculoViewModel? ObterPorId(int id);

    IEnumerable<VeiculoViewModel> ObterTodos();

    IEnumerable<VeiculoViewModel> ObterTodosPaginado(int size, int reg);

    bool Adicionar(VeiculoViewModel viewModel);

    bool Atualizar(VeiculoViewModel viewModel);

    bool Remover(int id);
}
