using Impacta.GestaoFrota.Application.ViewModel;

namespace Impacta.GestaoFrota.Application.Interfaces;

public interface IConsultaAppService
{
    IEnumerable<VeiculoDisponivelViewModel> ObterVeiculosDisponiveis();
    IEnumerable<VeiculoEmManutencaoViewModel> ObterVeiculosEmManutencao();
    IEnumerable<ReservaAprovadaConsultaViewModel> ObterReservasAprovadas();
    QuilometragemAtualFrotaViewModel ObterQuilometragemAtualFrota();
    IEnumerable<ValorTotalPorTipoEventoViewModel> ObterValorTotalPorTipoEvento();
    IEnumerable<BeneficiarioVeiculoConsultaViewModel> ObterBeneficiariosVeiculos();
    IEnumerable<RankingClienteReservasViewModel> ObterRankingClientesComMaisReservas();
    DashboardGerencialFrotaViewModel ObterIndicadoresDashboardFrota();
    IEnumerable<QuantidadeVeiculosPorFabricanteViewModel> ObterQuantidadeVeiculosPorFabricante();
    IEnumerable<UtilizacaoCompletaVeiculosViewModel> ObterUtilizacaoCompletaVeiculos();
}
