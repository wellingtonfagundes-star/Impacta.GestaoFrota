using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class LocalAssistenciaProfile : Profile
{
    public LocalAssistenciaProfile()
    {
        CreateMap<Local, LocalAssistenciaViewModel>();
        CreateMap<LocalAssistenciaViewModel, Local>();
    }
}
