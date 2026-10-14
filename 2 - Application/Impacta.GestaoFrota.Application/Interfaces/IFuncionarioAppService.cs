using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface IFuncionarioAppService
{
    FuncionarioViewModel? ObterPorId(int id);

    IEnumerable<FuncionarioViewModel> ObterTodos();

    bool Adicionar(FuncionarioViewModel viewModel);

    bool Atualizar(FuncionarioViewModel viewModel);

    bool Remover(int id);
}
