using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class StatusFrotaAppService : IStatusFrotaAppService
{
    private readonly IStatusFrotaService _service;
    private readonly IMapper _mapper;

    public StatusFrotaAppService(
        IStatusFrotaService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public StatusFrotaViewModel? ObterPorId(int id)
    {
        var statusFrota = _service.ObterPorId(id);
        if (statusFrota == null)
            return null;

        return _mapper.Map<StatusFrotaViewModel>(statusFrota);
    }

    public IEnumerable<StatusFrotaViewModel> ObterTodos()
    {
        var statusFrotas = _service.ObterTodos();
        return _mapper.Map<IEnumerable<StatusFrotaViewModel>>(statusFrotas);
    }

    public bool Adicionar(StatusFrotaViewModel viewModel)
    {
        try
        {
            var statusFrota = new StatusFrota
            {
                NomeStatus = viewModel.NomeStatus,
                Descricao = viewModel.Descricao
            };

            _service.Adicionar(statusFrota);
            _service.Salvar();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Atualizar(StatusFrotaViewModel viewModel)
    {
        try
        {
            var statusFrota = _service.ObterPorId(viewModel.IdStatusFrota);
            if (statusFrota == null)
                return false;

            statusFrota.NomeStatus = viewModel.NomeStatus;
            statusFrota.Descricao = viewModel.Descricao;

            _service.Atualizar(statusFrota);
            _service.Salvar();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Remover(int id)
    {
        try
        {
            return _service.Remover(id);
        }
        catch
        {
            return false;
        }
    }
}
