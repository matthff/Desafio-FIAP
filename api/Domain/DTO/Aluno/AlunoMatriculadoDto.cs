using System;

namespace Api.Domain.DTO.Aluno;

public class AlunoMatriculadoDto
{
    public AlunoDto Aluno { get; set; }

    public DateTime DataMatricula { get; set; }
}
