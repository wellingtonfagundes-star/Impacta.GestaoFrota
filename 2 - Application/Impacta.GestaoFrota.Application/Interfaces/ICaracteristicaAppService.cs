using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface ICaracteristicaAppService
{
    CaracteristicaViewModel? ObterPorId(int id);

    IEnumerable<CaracteristicaViewModel> ObterTodos();

    IEnumerable<CaracteristicaViewModel> ObterTodosPaginado(int size, int reg);

    bool Adicionar(CaracteristicaViewModel viewModel);

    bool Atualizar(CaracteristicaViewModel viewModel);

    bool Remover(int id);
}
