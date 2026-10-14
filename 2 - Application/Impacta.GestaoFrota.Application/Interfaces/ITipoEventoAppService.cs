using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface ITipoEventoAppService
{
    TipoEventoViewModel? ObterPorId(int id);
    IEnumerable<TipoEventoViewModel> ObterTodos();
    bool Adicionar(TipoEventoViewModel viewModel);
    bool Atualizar(TipoEventoViewModel viewModel);
    bool Remover(int id);
}
