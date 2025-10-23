using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Repository;

public interface IMatriculaRepository
{
    Task<IEnumerable<Matricula>> ObterMatriculasPorTurmaAsync(int turmaId);

    Task<Matricula> InserirMatriculaAsync(Matricula matricula);

    Task<bool> ExisteAlunoMatriculadoNaTurmaAsync(int alunoId, int turmaId);

    Task<bool> ExcluirAsync(int matriculaId);
}
