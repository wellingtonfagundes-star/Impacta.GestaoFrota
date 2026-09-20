using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class CaracteristicaVeiculoProfile : Profile
{
    public CaracteristicaVeiculoProfile()
    {
        // Mapear da entidade para ViewModel
        CreateMap<CaracteristicaVeiculo, CaracteristicaVeiculoViewModel>();

        CreateMap<CaracteristicaVeiculoViewModel, CaracteristicaVeiculo>()
            .ForMember(dest => dest.IdVeiculoNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.IdCaracteristicaNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore());
    }
}
