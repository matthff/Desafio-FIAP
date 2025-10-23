using System;
using Api.Domain.DTO.Aluno;

namespace Api.Domain.Entidades;

public class Matricula : EntidadeId
{
    public int AlunoId { get; private set; }
    public int TurmaId { get; private set; }
    public DateTime DataMatricula { get; private set; }
    public string Status { get; private set; }

    public Aluno Aluno { get; private set; }
    public Turma Turma { get; private set; }

    public Matricula(int alunoId, int turmaId)
    {
        AlunoId = alunoId;
        TurmaId = turmaId;
        DataMatricula = DateTime.UtcNow;
        Status = "Ativa";
    }
}
