using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface IClienteAppService
{
    ClienteViewModel? ObterPorId(int id);

    IEnumerable<ClienteViewModel> ObterTodos();

    bool Adicionar(ClienteViewModel viewModel);

    bool Atualizar(ClienteViewModel viewModel);

    bool Remover(int id);
}
