using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class TipoEventoAppService : ITipoEventoAppService
{
    private readonly ITipoEventoService _service;
    private readonly IMapper _mapper;

    public TipoEventoAppService(ITipoEventoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public TipoEventoViewModel? ObterPorId(int id)
    {
        var tipoEvento = _service.ObterPorId(id);
        return tipoEvento is null ? null : _mapper.Map<TipoEventoViewModel>(tipoEvento);
    }

    public IEnumerable<TipoEventoViewModel> ObterTodos()
    {
        return _mapper.Map<IEnumerable<TipoEventoViewModel>>(_service.ObterTodos());
    }

    public bool Adicionar(TipoEventoViewModel viewModel)
    {
        try
        {
            _service.Adicionar(_mapper.Map<TipoEvento>(viewModel));
            _service.Salvar();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Atualizar(TipoEventoViewModel viewModel)
    {
        try
        {
            var tipoEvento = _service.ObterPorId(viewModel.IdTipoevento);
            if (tipoEvento is null)
                return false;

            tipoEvento.Nome = viewModel.Nome;
            tipoEvento.Descricao = viewModel.Descricao;
            tipoEvento.InfoAdicional = viewModel.InfoAdicional;
            tipoEvento.Periodicidade = viewModel.Periodicidade;

            _service.Atualizar(tipoEvento);
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
