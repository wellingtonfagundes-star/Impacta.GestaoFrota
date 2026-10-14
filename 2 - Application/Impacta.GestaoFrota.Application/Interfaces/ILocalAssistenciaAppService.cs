using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface ILocalAssistenciaAppService
{
    LocalAssistenciaViewModel? ObterPorId(int id);

    IEnumerable<LocalAssistenciaViewModel> ObterTodos();

    bool Adicionar(LocalAssistenciaViewModel viewModel);

    bool Atualizar(LocalAssistenciaViewModel viewModel);

    bool Remover(int id);
}
