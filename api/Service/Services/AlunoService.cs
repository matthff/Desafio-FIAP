using Api.Dominio.DTO.Aluno;
using Api.Dominio.Entidades;
using Api.Dominio.Interfaces.Repository;
using Api.Dominio.Interfaces.Services;
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
