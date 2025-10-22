using Api.Domain.DTO.Aluno;
using E = Api.Domain.Entidades;
using AutoMapper;

namespace Api.CrossCutting.DtoMapper.Aluno;

public class AlunoProfile : Profile
{
    public AlunoProfile()
    {
        CreateMap<AlunoDto, E.Aluno>().ReverseMap();
    }
}

