using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<Funcionario, FuncionarioViewModel>()
            .ForMember(dest => dest.Sexo, opt => opt.MapFrom(src => src.Sexo.ToString()));

        CreateMap<FuncionarioViewModel, Funcionario>()
            .ForMember(dest => dest.Matricula, opt => opt.Ignore())
            .ForMember(dest => dest.Sexo, opt => opt.MapFrom(src => src.Sexo[0]));
    }
}
