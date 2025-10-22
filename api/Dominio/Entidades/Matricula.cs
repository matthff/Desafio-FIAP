using System;

namespace Api.Dominio.Entidades;

public class Matricula : EntidadeId
{
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public DateTime DataMatricula { get; set; }
    public string Status { get; set; }

    public Aluno Aluno { get; set; }
    public Turma Turma { get; set; }
}
