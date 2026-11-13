using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class HistoricoManutencaoAppService : IHistoricoManutencaoAppService
{
    private readonly IHistoricoManutencaoService _service;

    public HistoricoManutencaoAppService(IHistoricoManutencaoService service)
    {
        _service = service;
    }

    public IEnumerable<HistoricoManutencaoViewModel> ObterTodos() => _service.ObterTodos().Select(Mapear);

    public HistoricoManutencaoViewModel? ObterPorId(int id)
    {
        var historico = _service.ObterPorId(id);
        return historico is null ? null : Mapear(historico);
    }

    public IEnumerable<HistoricoManutencaoOpcaoViewModel> ObterLocais() => _service.ObterLocais()
        .Select(item => new HistoricoManutencaoOpcaoViewModel { Id = item.IdLocal, Nome = item.Nome });

    public IEnumerable<HistoricoManutencaoOpcaoViewModel> ObterTiposEvento() => _service.ObterTiposEvento()
        .Select(item => new HistoricoManutencaoOpcaoViewModel { Id = item.IdTipoevento, Nome = item.Nome });

    public IEnumerable<HistoricoManutencaoOpcaoViewModel> ObterFuncionarios() => _service.ObterFuncionarios()
        .Select(item => new HistoricoManutencaoOpcaoViewModel { Id = item.IdFuncionario, Nome = item.Nome });

    public IEnumerable<HistoricoManutencaoOpcaoViewModel> ObterClientes() => _service.ObterClientes()
        .Select(item => new HistoricoManutencaoOpcaoViewModel { Id = item.IdPessoa, Nome = item.Nome });

    public bool Adicionar(HistoricoManutencaoViewModel viewModel)
    {
        if (!ReferenciasValidas(viewModel))
            return false;

        return _service.Adicionar(CriarEntidade(viewModel));
    }

    public bool Atualizar(HistoricoManutencaoViewModel viewModel)
    {
        if (!ReferenciasValidas(viewModel))
            return false;

        var historico = _service.ObterPorId(viewModel.IdHistorico);
        if (historico is null)
            return false;

        historico.IdLocal = viewModel.IdLocal;
        historico.IdTipoevento = viewModel.IdTipoEvento;
        historico.IdFuncionarioResponsavel = viewModel.IdFuncionarioResponsavel;
        historico.IdClienteAtendido = viewModel.IdClienteAtendido;
        historico.DataEvento = viewModel.DataEvento;
        historico.DescrEvento = viewModel.DescrEvento.Trim();
        historico.Quilometragem = viewModel.Quilometragem!.Value;
        historico.Valor = viewModel.Valor;
        historico.NrNotaFiscal = viewModel.NrNotaFiscal!.Value;
        historico.Obs = viewModel.Obs;

        return _service.Atualizar(historico);
    }

    public bool Remover(int id) => _service.Remover(id);

    private bool ReferenciasValidas(HistoricoManutencaoViewModel viewModel)
    {
        return _service.ObterLocais().Any(item => item.IdLocal == viewModel.IdLocal)
            && _service.ObterTiposEvento().Any(item => item.IdTipoevento == viewModel.IdTipoEvento)
            && _service.ObterFuncionarios().Any(item => item.IdFuncionario == viewModel.IdFuncionarioResponsavel)
            && _service.ObterClientes().Any(item => item.IdPessoa == viewModel.IdClienteAtendido);
    }

    private static Historico CriarEntidade(HistoricoManutencaoViewModel viewModel)
    {
        return new Historico
        {
            IdLocal = viewModel.IdLocal,
            IdTipoevento = viewModel.IdTipoEvento,
            IdFuncionarioResponsavel = viewModel.IdFuncionarioResponsavel,
            IdClienteAtendido = viewModel.IdClienteAtendido,
            DataEvento = viewModel.DataEvento,
            DescrEvento = viewModel.DescrEvento.Trim(),
            Quilometragem = viewModel.Quilometragem!.Value,
            Valor = viewModel.Valor,
            NrNotaFiscal = viewModel.NrNotaFiscal!.Value,
            Obs = viewModel.Obs
        };
    }

    private static HistoricoManutencaoViewModel Mapear(Historico historico)
    {
        return new HistoricoManutencaoViewModel
        {
            IdHistorico = historico.IdHistorico,
            IdLocal = historico.IdLocal,
            IdTipoEvento = historico.IdTipoevento,
            IdFuncionarioResponsavel = historico.IdFuncionarioResponsavel,
            IdClienteAtendido = historico.IdClienteAtendido,
            DataEvento = historico.DataEvento,
            DescrEvento = historico.DescrEvento,
            Quilometragem = historico.Quilometragem,
            Valor = historico.Valor,
            NrNotaFiscal = historico.NrNotaFiscal,
            Obs = historico.Obs,
            LocalNome = historico.IdLocalNavigation?.Nome ?? string.Empty,
            TipoEventoNome = historico.IdTipoeventoNavigation?.Nome ?? string.Empty,
            FuncionarioResponsavelNome = historico.IdFuncionarioResponsavelNavigation?.Nome ?? string.Empty,
            ClienteNome = historico.IdClienteAtendidoNavigation?.Nome ?? string.Empty
        };
    }
}
