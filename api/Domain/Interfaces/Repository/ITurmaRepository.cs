using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Repository;

public interface ITurmaRepository : IBaseRepository<Turma>
{
    Task<IEnumerable<Turma>> ObterTodosComAlunosAsync();

    Task<Turma> ObterPorIdComAlunosAsync(int turmaId);
}
