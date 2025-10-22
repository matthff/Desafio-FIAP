using Api.Domain.DTO.Turma;
using AutoMapper;
using E = Api.Domain.Entidades;

namespace Api.CrossCutting.DtoMapper.Turma;

public class TurmaProfile : Profile
{
    public TurmaProfile()
    {
        CreateMap<E.Turma, TurmaDto>()
            .ForMember(dest => dest.QuantidadeDeAlunos,
                opt => opt.MapFrom(src => src.QuantidadeDeAlunosNaTurma));

        CreateMap<TurmaDto, E.Turma>();

        CreateMap<TurmaInserirDto, E.Turma>().ReverseMap();

        CreateMap<TurmaAtualizarDto, E.Turma>().ReverseMap();
    }
}

