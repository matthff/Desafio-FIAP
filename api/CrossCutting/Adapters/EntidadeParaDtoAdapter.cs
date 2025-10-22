using Api.Domain.DTO.Aluno;
using Api.Domain.DTO.Turma;
using Api.Domain.Entidades;
using AutoMapper;

namespace Api.CrossCutting.Mapping
{
    public class EntidadeParaDtoAdapter : Profile
    {
        public EntidadeParaDtoAdapter()
        {
            CreateMap<AlunoDto, Aluno>().ReverseMap();


            CreateMap<Turma, TurmaDto>()
                .ForMember(dest => dest.QuantidadeDeAlunos,
                    opt => opt.MapFrom(src => src.QuantidadeDeAlunosNaTurma));

            CreateMap<TurmaDto, Turma>();
        }
    }
}
