using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dominio.DTO.Turma;

namespace Api.Dominio.Interfaces.Services;

public interface ITurmaService : IBaseService<TurmaDto>
{
    Task<IEnumerable<TurmaDto>> ObterTodosComQuantidadeDeAlunos();
}

