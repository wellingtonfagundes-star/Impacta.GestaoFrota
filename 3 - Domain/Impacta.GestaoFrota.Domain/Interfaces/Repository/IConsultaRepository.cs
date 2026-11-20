using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Interfaces.Repository;

public interface IConsultaRepository
{
    IEnumerable<VeiculoDisponivelConsulta> ObterVeiculosDisponiveis();
    IEnumerable<VeiculoEmManutencaoConsulta> ObterVeiculosEmManutencao();
    IEnumerable<ReservaAprovadaConsulta> ObterReservasAprovadas();
    QuilometragemAtualFrotaConsulta ObterQuilometragemAtualFrota();
    IEnumerable<ValorTotalPorTipoEventoConsulta> ObterValorTotalPorTipoEvento();
    IEnumerable<BeneficiarioVeiculoConsulta> ObterBeneficiariosVeiculos();
    IEnumerable<RankingClienteReservasConsulta> ObterRankingClientesComMaisReservas();
    DashboardGerencialFrotaConsulta ObterIndicadoresDashboardFrota();
    IEnumerable<QuantidadeVeiculosFabricanteRelatorio> ObterQuantidadeVeiculosPorFabricante();
    IEnumerable<UtilizacaoCompletaVeiculosRelatorio> ObterUtilizacaoCompletaVeiculos();
}
