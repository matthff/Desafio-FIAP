using Api.Dominio.DTO.Aluno;
using Api.Dominio.DTO.Turma;
using Api.Dominio.Entidades;
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
