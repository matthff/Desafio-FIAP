using Api.Domain.DTO.Aluno;
using E = Api.Domain.Entidades;
using AutoMapper;
using Api.Domain.DTO.Administrador;

namespace Api.CrossCutting.DtoMapper.Administrador;

public class AdministradorProfile : Profile
{
    public AdministradorProfile()
    {
        CreateMap<AdministradorDto, E.Administrador>().ReverseMap();

        CreateMap<E.Administrador, AdministradorInserirDto>();

        CreateMap<AdministradorInserirDto, E.Administrador>()
            .ForMember(dest => dest.SenhaHash,
                opt => opt.MapFrom(src => src.Senha));

        CreateMap<AdministradorAtualizarDto, E.Administrador>().ReverseMap();

        CreateMap<AdministradorLoginDto, E.Administrador>()
            .ForMember(dest => dest.SenhaHash,
                opt => opt.MapFrom(src => src.Senha));
    }
}

