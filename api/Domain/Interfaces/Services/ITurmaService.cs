using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.Turma;

namespace Api.Domain.Interfaces.Services;

public interface ITurmaService : IBaseService<TurmaDto>
{
    Task<IEnumerable<TurmaDto>> ObterTodosComQuantidadeDeAlunos();

    Task<TurmaDto> ObterPorIdComQuantidadeDeAlunos(int turmaId);

    Task<TurmaDto> InserirTurma(TurmaCreateDto turmaCriada);

    Task<TurmaDto> AtualizarTurma(TurmaAtualizarDto turmaAtualizada);

    Task<bool> ExcluirTurma(int turmaId);
}

