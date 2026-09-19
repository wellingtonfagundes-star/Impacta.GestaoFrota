using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class CaracteristicaProfile : Profile
{
    public CaracteristicaProfile()
    {
        CreateMap<Caracteristica, CaracteristicaViewModel>();
        CreateMap<CaracteristicaViewModel, Caracteristica>();
    }
}
