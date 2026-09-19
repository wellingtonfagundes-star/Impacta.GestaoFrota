using AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.Services;

public class CaracteristicaAppService : ICaracteristicaAppService
{
    private readonly ICaracteristicaService _caracteristicaService;
    private readonly IMapper _mapper;

    public CaracteristicaAppService(ICaracteristicaService caracteristicaService, IMapper mapper)
    {
        _caracteristicaService = caracteristicaService;
        _mapper = mapper;
    }

    public CaracteristicaViewModel? ObterPorId(int id)
    {
        var caracteristica = _caracteristicaService.ObterPorId(id);
        return caracteristica is null ? null : _mapper.Map<CaracteristicaViewModel>(caracteristica);
    }

    public IEnumerable<CaracteristicaViewModel> ObterTodos()
    {
        return _mapper.Map<IEnumerable<CaracteristicaViewModel>>(_caracteristicaService.ObterTodos());
    }

    public IEnumerable<CaracteristicaViewModel> ObterTodosPaginado(int size, int reg)
    {
        return _mapper.Map<IEnumerable<CaracteristicaViewModel>>(
            _caracteristicaService.ObterTodosPaginado(size, reg));
    }

    public bool Adicionar(CaracteristicaViewModel viewModel)
    {
        return _caracteristicaService.Adicionar(_mapper.Map<Caracteristica>(viewModel));
    }

    public bool Atualizar(CaracteristicaViewModel viewModel)
    {
        return _caracteristicaService.Atualizar(_mapper.Map<Caracteristica>(viewModel));
    }

    public bool Remover(int id)
    {
        return _caracteristicaService.Remover(id);
    }
}
