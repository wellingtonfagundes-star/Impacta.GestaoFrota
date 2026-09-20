using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class CaracteristicaProfile : Profile
{
    public CaracteristicaProfile()
    {
        // Mapeamento da entidade para ViewModel - converte bool? para bool com valor padrão false
        CreateMap<Caracteristica, CaracteristicaViewModel>()
            .ForMember(dest => dest.Opcional, opt => opt.MapFrom(src => src.Opcional ?? false));

        // Mapeamento do ViewModel para entidade - mapeia bool para bool?
        CreateMap<CaracteristicaViewModel, Caracteristica>();
    }
}
