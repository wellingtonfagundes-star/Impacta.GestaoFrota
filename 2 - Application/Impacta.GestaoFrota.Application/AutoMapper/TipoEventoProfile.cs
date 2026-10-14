using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class TipoEventoProfile : Profile
{
    public TipoEventoProfile()
    {
        CreateMap<TipoEvento, TipoEventoViewModel>();
        CreateMap<TipoEventoViewModel, TipoEvento>()
            .ForMember(dest => dest.Historicos, opt => opt.Ignore());
    }
}
