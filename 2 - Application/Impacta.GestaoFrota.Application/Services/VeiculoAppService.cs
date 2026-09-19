using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class VeiculoAppService : IVeiculoAppService
{
    private readonly IVeiculoService _veiculoService;
    private readonly IMapper _mapper;

    public VeiculoAppService(IVeiculoService veiculoService, IMapper mapper)
    {
        _veiculoService = veiculoService;
        _mapper = mapper;
    }

    public VeiculoViewModel? ObterPorId(int id)
    {
        var veiculo = _veiculoService.ObterPorId(id);
        return veiculo is null ? null : _mapper.Map<VeiculoViewModel>(veiculo);
    }

    public IEnumerable<VeiculoViewModel> ObterTodos()
    {
        return _mapper.Map<IEnumerable<VeiculoViewModel>>(_veiculoService.ObterTodos());
    }

    public IEnumerable<VeiculoViewModel> ObterTodosPaginado(int size, int reg)
    {
        return _mapper.Map<IEnumerable<VeiculoViewModel>>(
            _veiculoService.ObterTodosPaginado(size, reg));
    }

    public bool Adicionar(VeiculoViewModel viewModel)
    {
        return _veiculoService.Adicionar(_mapper.Map<Veiculo>(viewModel));
    }

    public bool Atualizar(VeiculoViewModel viewModel)
    {
        return _veiculoService.Atualizar(_mapper.Map<Veiculo>(viewModel));
    }

    public bool Remover(int id)
    {
        return _veiculoService.Remover(id);
    }
}
