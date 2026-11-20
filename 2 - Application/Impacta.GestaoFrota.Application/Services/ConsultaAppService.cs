using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;

namespace Impacta.GestaoFrota.Application.Services;

public class ConsultaAppService : IConsultaAppService
{
    private readonly IConsultaService _service;

    public ConsultaAppService(IConsultaService service)
    {
        _service = service;
    }

    public IEnumerable<VeiculoDisponivelViewModel> ObterVeiculosDisponiveis() => _service.ObterVeiculosDisponiveis()
        .Select(item => new VeiculoDisponivelViewModel
        {
            IdVeiculo = item.IdVeiculo,
            Placa = item.Placa,
            Fabricante = item.Fabricante,
            AnoModelo = item.AnoModelo
        });

    public IEnumerable<VeiculoEmManutencaoViewModel> ObterVeiculosEmManutencao() => _service.ObterVeiculosEmManutencao()
        .Select(item => new VeiculoEmManutencaoViewModel
        {
            Placa = item.Placa,
            Fabricante = item.Fabricante,
            AnoModelo = item.AnoModelo,
            NomeStatus = item.NomeStatus
        });

    public IEnumerable<ReservaAprovadaConsultaViewModel> ObterReservasAprovadas() => _service.ObterReservasAprovadas()
        .Select(item => new ReservaAprovadaConsultaViewModel
        {
            IdReserva = item.IdReserva,
            Solicitante = item.Solicitante,
            Aprovador = item.Aprovador,
            Data = item.Data,
            KmEstimada = item.KmEstimada
        });

    public QuilometragemAtualFrotaViewModel ObterQuilometragemAtualFrota()
    {
        var item = _service.ObterQuilometragemAtualFrota();
        return new QuilometragemAtualFrotaViewModel
        {
            KmTotalFrota = item.KmTotalFrota,
            KmMediaFrota = item.KmMediaFrota
        };
    }

    public IEnumerable<ValorTotalPorTipoEventoViewModel> ObterValorTotalPorTipoEvento() => _service.ObterValorTotalPorTipoEvento()
        .Select(item => new ValorTotalPorTipoEventoViewModel
        {
            Nome = item.Nome,
            TotalGasto = item.TotalGasto
        });

    public IEnumerable<BeneficiarioVeiculoConsultaViewModel> ObterBeneficiariosVeiculos() => _service.ObterBeneficiariosVeiculos()
        .Select(item => new BeneficiarioVeiculoConsultaViewModel
        {
            IdBeneficio = item.IdBeneficio,
            Beneficiado = item.Beneficiado,
            Motorista = item.Motorista,
            Placa = item.Placa,
            DataEntrega = item.DataEntrega,
            DataDevolucao = item.DataDevolucao
        });

    public IEnumerable<RankingClienteReservasViewModel> ObterRankingClientesComMaisReservas() => _service.ObterRankingClientesComMaisReservas()
        .Select(item => new RankingClienteReservasViewModel
        {
            Nome = item.Nome,
            TotalReservas = item.TotalReservas
        });

    public DashboardGerencialFrotaViewModel ObterIndicadoresDashboardFrota()
    {
        var item = _service.ObterIndicadoresDashboardFrota();
        return new DashboardGerencialFrotaViewModel
        {
            TotalVeiculos = item.TotalVeiculos,
            Disponiveis = item.Disponiveis,
            Reservados = item.Reservados,
            Manutencao = item.Manutencao
        };
    }

    public IEnumerable<QuantidadeVeiculosPorFabricanteViewModel> ObterQuantidadeVeiculosPorFabricante() => _service.ObterQuantidadeVeiculosPorFabricante()
        .Select(item => new QuantidadeVeiculosPorFabricanteViewModel
        {
            Fabricante = item.Fabricante,
            Quantidade = item.Quantidade
        });

    public IEnumerable<UtilizacaoCompletaVeiculosViewModel> ObterUtilizacaoCompletaVeiculos() => _service.ObterUtilizacaoCompletaVeiculos()
        .Select(item => new UtilizacaoCompletaVeiculosViewModel
        {
            Placa = item.Placa,
            Fabricante = item.Fabricante,
            AnoModelo = item.AnoModelo,
            NomeStatus = item.NomeStatus,
            TotalReservas = item.TotalReservas,
            UltimoEvento = item.UltimoEvento
        });
}
