using Api.Domain.DTO.Aluno;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services
{
    public class AlunoService : BaseService<Aluno, AlunoDto>, IAlunoService
    {
        public AlunoService(IAlunoRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
