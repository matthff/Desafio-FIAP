using System.Threading.Tasks;
using Api.Domain.DTO.Matricula;
using Api.Domain.DTO.Turma;

namespace Api.Domain.Interfaces.Services;

public interface IMatriculaService
{
    Task MatricularAlunoAsync(MatriculaDoAlunoDto matriculaDoAluno);

    Task<MatriculasDaTurmaDto> ObterMatriculasDaTurmaAsync(int turmaId);

    Task<bool> ExcluirMatriculaAsync(int matriculaId);
}

