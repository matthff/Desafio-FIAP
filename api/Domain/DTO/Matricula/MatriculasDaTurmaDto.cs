using System.Collections.Generic;
using Api.Domain.DTO.Aluno;

namespace Api.Domain.DTO.Matricula;

public class MatriculasDaTurmaDto
{
    public string NomeDaTurma { get; set; }
    public IEnumerable<AlunoMatriculadoDto> AlunosMatriculados { get; set; }
}
