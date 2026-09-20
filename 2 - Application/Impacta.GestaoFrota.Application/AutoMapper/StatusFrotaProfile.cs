using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class StatusFrotaProfile : Profile
{
    public StatusFrotaProfile()
    {
        CreateMap<StatusFrota, StatusFrotaViewModel>();
        CreateMap<StatusFrotaViewModel, StatusFrota>();
    }
}
