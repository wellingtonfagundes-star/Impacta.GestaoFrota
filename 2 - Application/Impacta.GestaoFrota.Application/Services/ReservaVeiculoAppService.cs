using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class ReservaVeiculoAppService : IReservaVeiculoAppService
{
    private readonly IReservaVeiculoService _service;

    public ReservaVeiculoAppService(IReservaVeiculoService service)
    {
        _service = service;
    }

    public IEnumerable<ReservaVeiculoViewModel> ObterTodos() => _service.ObterTodosComRelacionamentos().Select(Mapear);

    public ReservaVeiculoViewModel? ObterPorId(int id)
    {
        var reserva = _service.ObterPorIdComRelacionamentos(id);
        return reserva is null ? null : Mapear(reserva);
    }

    public IEnumerable<ReservaVeiculoOpcaoViewModel> ObterVeiculos() => _service.ObterVeiculos()
        .Select(item => new ReservaVeiculoOpcaoViewModel
        {
            Id = item.IdVeiculo,
            Nome = $"{item.Nome} - {item.Placa} - {item.Fabricante} - {item.AnoModelo}"
        });

    public IEnumerable<ReservaVeiculoOpcaoViewModel> ObterLocais() => _service.ObterLocais()
        .Select(item => new ReservaVeiculoOpcaoViewModel { Id = item.IdLocal, Nome = item.Nome });

    public IEnumerable<ReservaVeiculoOpcaoViewModel> ObterPessoas() => _service.ObterPessoas()
        .Select(item => new ReservaVeiculoOpcaoViewModel { Id = item.IdPessoa, Nome = item.Nome });

    public IEnumerable<ReservaVeiculoOpcaoViewModel> ObterFuncionarios() => _service.ObterFuncionarios()
        .Select(item => new ReservaVeiculoOpcaoViewModel { Id = item.IdFuncionario, Nome = item.Nome });

    public bool Adicionar(ReservaVeiculoViewModel viewModel)
    {
        if (!ReferenciasValidas(viewModel, incluirRetorno: false))
            return false;

        var reserva = new ReservaVeiculo();
        AtualizarReserva(reserva, viewModel);
        var retorno = new RetornoVeiculo
        {
            IdVeiculo = viewModel.IdVeiculo,
            DataRetirada = viewModel.Data
        };

        return _service.Salvar(reserva, retorno);
    }

    public bool Atualizar(ReservaVeiculoViewModel viewModel)
    {
        var reserva = _service.ObterPorIdComRelacionamentos(viewModel.IdReserva);
        if (reserva is null)
            return false;

        var retorno = reserva.RetornoVeiculos.FirstOrDefault() ?? new RetornoVeiculo();
        var dataRetorno = retorno.DataRetorno ?? viewModel.DataRetorno;
        if (!ReferenciasValidas(viewModel, dataRetorno.HasValue))
            return false;

        AtualizarReserva(reserva, viewModel);
        retorno.IdVeiculo = viewModel.IdVeiculo;
        retorno.DataRetirada = viewModel.Data;
        retorno.DataRetorno = dataRetorno;
        retorno.IdLocalRetorno = dataRetorno.HasValue ? viewModel.IdLocalRetorno : null;
        retorno.KmRetorno = dataRetorno.HasValue ? viewModel.KmEntregue : null;

        if (dataRetorno.HasValue)
        {
            var status = retorno.IdStatusNavigation ?? new StatusVeiculo();
            status.KmEntregue = (double)viewModel.KmEntregue!.Value;
            status.Combustivel = viewModel.Combustivel!.Trim();
            status.Lataria = viewModel.Lataria!.Trim();
            status.Pneus = viewModel.Pneus!.Trim();
            status.ObsGeral = viewModel.ObsRetorno;
            retorno.IdStatusNavigation = status;
        }
        else
        {
            retorno.IdLocalRetorno = null;
            retorno.IdStatus = null;
            retorno.IdStatusNavigation = null;
            retorno.KmRetorno = null;
        }

        return _service.Salvar(reserva, retorno);
    }

    public bool Remover(int id) => _service.Remover(id);

    private bool ReferenciasValidas(ReservaVeiculoViewModel viewModel, bool incluirRetorno)
    {
        var veiculos = _service.ObterVeiculos().Select(item => item.IdVeiculo).ToHashSet();
        var locais = _service.ObterLocais().Select(item => item.IdLocal).ToHashSet();
        var pessoas = _service.ObterPessoas().Select(item => item.IdPessoa).ToHashSet();
        var funcionarios = _service.ObterFuncionarios().Select(item => item.IdFuncionario).ToHashSet();

        return veiculos.Contains(viewModel.IdVeiculo)
            && locais.Contains(viewModel.IdLocalDestino)
            && pessoas.Contains(viewModel.IdClienteSolicitante)
            && funcionarios.Contains(viewModel.IdFuncionarioAprovador)
            && (!incluirRetorno || (viewModel.IdLocalRetorno.HasValue && locais.Contains(viewModel.IdLocalRetorno.Value)
                && viewModel.KmEntregue.HasValue
                && !string.IsNullOrWhiteSpace(viewModel.Combustivel) && viewModel.Combustivel.Length <= 20
                && !string.IsNullOrWhiteSpace(viewModel.Lataria) && viewModel.Lataria.Length <= 100
                && !string.IsNullOrWhiteSpace(viewModel.Pneus) && viewModel.Pneus.Length <= 50));
    }

    private static void AtualizarReserva(ReservaVeiculo reserva, ReservaVeiculoViewModel viewModel)
    {
        reserva.IdLocalDestino = viewModel.IdLocalDestino;
        reserva.IdClienteSolicitante = viewModel.IdClienteSolicitante;
        reserva.Data = viewModel.Data;
        reserva.IdFuncionarioAprovador = viewModel.IdFuncionarioAprovador;
        reserva.Aprovado = viewModel.Aprovado;
        reserva.Motivo = viewModel.Motivo;
        reserva.KmEstimada = (double?)viewModel.KmEstimada;
        reserva.Estacionamento = viewModel.Estacionamento;
        reserva.Capacete = viewModel.Capacete;
        reserva.Gps = viewModel.Gps;
        reserva.EqpSeguranca = viewModel.EqpSeguranca;
        reserva.Emergencia = viewModel.Emergencia;
        reserva.ObsGeral = viewModel.ObsGeral;
    }

    private static ReservaVeiculoViewModel Mapear(ReservaVeiculo reserva)
    {
        var retorno = reserva.RetornoVeiculos.FirstOrDefault();
        var veiculo = retorno?.IdVeiculoNavigation;
        return new ReservaVeiculoViewModel
        {
            IdReserva = reserva.IdReserva,
            IdRetorno = retorno?.IdRetorno,
            IdVeiculo = retorno?.IdVeiculo ?? 0,
            IdLocalDestino = reserva.IdLocalDestino ?? 0,
            IdClienteSolicitante = reserva.IdClienteSolicitante ?? 0,
            Data = reserva.Data,
            IdFuncionarioAprovador = reserva.IdFuncionarioAprovador ?? 0,
            Aprovado = reserva.Aprovado ?? false,
            Motivo = reserva.Motivo,
            KmEstimada = (decimal?)reserva.KmEstimada,
            Estacionamento = reserva.Estacionamento ?? false,
            Capacete = reserva.Capacete ?? false,
            Gps = reserva.Gps ?? false,
            EqpSeguranca = reserva.EqpSeguranca ?? false,
            Emergencia = reserva.Emergencia ?? false,
            ObsGeral = reserva.ObsGeral,
            IdLocalRetorno = retorno?.IdLocalRetorno,
            DataRetorno = retorno?.DataRetorno,
            KmEntregue = retorno?.IdStatusNavigation?.KmEntregue is double km ? (decimal)km : null,
            Combustivel = retorno?.IdStatusNavigation?.Combustivel,
            Lataria = retorno?.IdStatusNavigation?.Lataria,
            Pneus = retorno?.IdStatusNavigation?.Pneus,
            ObsRetorno = retorno?.IdStatusNavigation?.ObsGeral,
            VeiculoNome = veiculo?.Nome ?? string.Empty,
            VeiculoPlaca = veiculo?.Placa ?? string.Empty,
            ClienteNome = reserva.IdClienteSolicitanteNavigation?.Nome ?? string.Empty,
            FuncionarioAprovadorNome = reserva.IdFuncionarioAprovadorNavigation?.Nome ?? string.Empty,
            LocalDestinoNome = reserva.IdLocalDestinoNavigation?.Nome ?? string.Empty,
            LocalRetornoNome = retorno?.IdLocalRetornoNavigation?.Nome ?? string.Empty,
            KmRetorno = retorno?.KmRetorno
        };
    }
}
