using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface IVeiculoBeneficioAppService
{
    IEnumerable<VeiculoBeneficioViewModel> ObterTodos();
    VeiculoBeneficioViewModel? ObterPorId(int id);
    IEnumerable<PessoaOpcaoViewModel> ObterPessoas();
    IEnumerable<VeiculoOpcaoViewModel> ObterVeiculos();
    bool Adicionar(VeiculoBeneficioViewModel viewModel);
    bool Atualizar(VeiculoBeneficioViewModel viewModel);
    bool Remover(int id);
}