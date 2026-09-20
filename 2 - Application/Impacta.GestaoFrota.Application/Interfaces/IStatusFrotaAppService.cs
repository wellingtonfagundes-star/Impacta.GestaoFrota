using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface IStatusFrotaAppService
{
    StatusFrotaViewModel? ObterPorId(int id);

    IEnumerable<StatusFrotaViewModel> ObterTodos();

    bool Adicionar(StatusFrotaViewModel viewModel);

    bool Atualizar(StatusFrotaViewModel viewModel);

    bool Remover(int id);
}
