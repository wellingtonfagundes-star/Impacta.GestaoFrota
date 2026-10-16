using AutoMapper;
using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Domain.Models;

namespace Impacta.GestaoFrota.Application.AutoMapper;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Pessoa, ClienteViewModel>()
            .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.TipoCliente == "PJ" ? src.Cnpj : src.Cpf))
            .ForMember(dest => dest.Sexo, opt => opt.MapFrom(src => src.Sexo.ToString()));

        CreateMap<ClienteViewModel, Pessoa>()
            .ForMember(dest => dest.Cpf, opt => opt.Ignore())
            .ForMember(dest => dest.Cnpj, opt => opt.Ignore())
            .ForMember(dest => dest.Sexo, opt => opt.MapFrom(src => src.Sexo[0]));
    }
}
