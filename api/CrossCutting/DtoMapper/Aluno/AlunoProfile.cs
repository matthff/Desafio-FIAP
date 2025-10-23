using Api.Domain.DTO.Aluno;
using E = Api.Domain.Entidades;
using AutoMapper;

namespace Api.CrossCutting.DtoMapper.Aluno;

public class AlunoProfile : Profile
{
    public AlunoProfile()
    {
        CreateMap<AlunoDto, E.Aluno>().ReverseMap();

        CreateMap<E.Aluno, AlunoInserirDto>();

        CreateMap<AlunoInserirDto, E.Aluno>()
            .ForMember(dest => dest.SenhaHash,
                opt => opt.MapFrom(src => src.Senha));

        CreateMap<E.Aluno, AlunoAtualizarDto>();

        CreateMap<AlunoAtualizarDto, E.Aluno>()
            .ForMember(dest => dest.SenhaHash,
                opt => opt.MapFrom(src => src.Senha));
    }
}

