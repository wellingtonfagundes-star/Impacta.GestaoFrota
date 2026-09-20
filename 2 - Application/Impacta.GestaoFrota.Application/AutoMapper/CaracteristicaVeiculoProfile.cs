using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class CaracteristicaVeiculoProfile : Profile
{
    public CaracteristicaVeiculoProfile()
    {
        CreateMap<CaracteristicaVeiculo, CaracteristicaVeiculoViewModel>()
            .ForMember(dest => dest.VeiculoDescricao,
                opt => opt.MapFrom(src => src.IdVeiculoNavigation != null ? $"{src.IdVeiculoNavigation.Placa} - {src.IdVeiculoNavigation.Fabricante}" : ""))
            .ForMember(dest => dest.CaracteristicaDescricao,
                opt => opt.MapFrom(src => src.IdCaracteristicaNavigation != null ? src.IdCaracteristicaNavigation.Descricao : ""));

        CreateMap<CaracteristicaVeiculoViewModel, CaracteristicaVeiculo>()
            .ForMember(dest => dest.IdVeiculoNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.IdCaracteristicaNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore());

    }
}
