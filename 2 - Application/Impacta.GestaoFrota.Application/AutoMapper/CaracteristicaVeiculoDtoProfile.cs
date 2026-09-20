using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class CaracteristicaVeiculoDtoProfile : Profile
{
    public CaracteristicaVeiculoDtoProfile()
    {
        CreateMap<CaracteristicaVeiculoDto, CaracteristicaVeiculoViewModel>();
    }
}
