using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Domain.Services;

public class ConsultaService : IConsultaService
{
    private readonly IConsultaRepository _repository;

    public ConsultaService(IConsultaRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<VeiculoDisponivelConsulta> ObterVeiculosDisponiveis() => _repository.ObterVeiculosDisponiveis();
    public IEnumerable<VeiculoEmManutencaoConsulta> ObterVeiculosEmManutencao() => _repository.ObterVeiculosEmManutencao();
    public IEnumerable<ReservaAprovadaConsulta> ObterReservasAprovadas() => _repository.ObterReservasAprovadas();
    public QuilometragemAtualFrotaConsulta ObterQuilometragemAtualFrota() => _repository.ObterQuilometragemAtualFrota();
    public IEnumerable<ValorTotalPorTipoEventoConsulta> ObterValorTotalPorTipoEvento() => _repository.ObterValorTotalPorTipoEvento();
    public IEnumerable<BeneficiarioVeiculoConsulta> ObterBeneficiariosVeiculos() => _repository.ObterBeneficiariosVeiculos();
    public IEnumerable<RankingClienteReservasConsulta> ObterRankingClientesComMaisReservas() => _repository.ObterRankingClientesComMaisReservas();
    public DashboardGerencialFrotaConsulta ObterIndicadoresDashboardFrota() => _repository.ObterIndicadoresDashboardFrota();
    public IEnumerable<QuantidadeVeiculosFabricanteRelatorio> ObterQuantidadeVeiculosPorFabricante() => _repository.ObterQuantidadeVeiculosPorFabricante();
    public IEnumerable<UtilizacaoCompletaVeiculosRelatorio> ObterUtilizacaoCompletaVeiculos() => _repository.ObterUtilizacaoCompletaVeiculos();
}
