using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface ICaracteristicaVeiculoAppService
{
    CaracteristicaVeiculoViewModel? ObterPorId(int idVeiculo, int idCaracteristica);
    IEnumerable<CaracteristicaVeiculoViewModel> ObterTodos();
    bool Adicionar(CaracteristicaVeiculoViewModel viewModel);
    bool Atualizar(CaracteristicaVeiculoViewModel viewModel);
    bool Remover(int idVeiculo, int idCaracteristica);
}
