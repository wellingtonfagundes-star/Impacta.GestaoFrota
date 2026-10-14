using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class LocalAssistenciaAppService : ILocalAssistenciaAppService
{
    private readonly ILocalAssistenciaService _service;
    private readonly IMapper _mapper;

    public LocalAssistenciaAppService(ILocalAssistenciaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public LocalAssistenciaViewModel? ObterPorId(int id)
    {
        var local = _service.ObterPorId(id);
        return local is null ? null : _mapper.Map<LocalAssistenciaViewModel>(local);
    }

    public IEnumerable<LocalAssistenciaViewModel> ObterTodos()
    {
        return _mapper.Map<IEnumerable<LocalAssistenciaViewModel>>(_service.ObterTodos());
    }

    public bool Adicionar(LocalAssistenciaViewModel viewModel)
    {
        try
        {
            return _service.Adicionar(_mapper.Map<Local>(viewModel));
        }
        catch
        {
            return false;
        }
    }

    public bool Atualizar(LocalAssistenciaViewModel viewModel)
    {
        try
        {
            var local = _service.ObterPorId(viewModel.IdLocal);
            if (local is null)
                return false;

            _mapper.Map(viewModel, local);
            return _service.Atualizar(local);
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
