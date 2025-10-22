using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.Turma;

namespace Api.Domain.Interfaces.Services;

public interface ITurmaService : IBaseService<TurmaDto>
{
    Task<IEnumerable<TurmaDto>> ObterTodosComQuantidadeDeAlunos();

    Task<TurmaDto> InserirTurma(TurmaCreateDto trainerCreated);
}

