using System.Collections.Generic;
using System.Linq;

namespace Api.Domain.Entidades;

public class Turma : EntidadeBase
{
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public IEnumerable<Aluno> Alunos { get; set; }
    public IEnumerable<Matricula> Matriculas { get; set; }

    public int QuantidadeDeAlunosNaTurma => Alunos != null ? Alunos.Count() : 0;
}
