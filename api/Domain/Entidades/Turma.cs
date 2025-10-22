using System.Collections.Generic;
using System.Linq;

namespace Api.Domain.Entidades;

public class Turma : EntidadeBase
{
    public const int TamanhoMinimoNome = 3;
    public const int TamanhoMaximoNome = 100;
    public const int TamanhoMinimoDescricao = 10;
    public const int TamanhoMaximoDescricao = 250;

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeDeAlunosNaTurma => Alunos != null ? Alunos.Count() : 0;

    public IEnumerable<Aluno> Alunos { get; set; }
    public IEnumerable<Matricula> Matriculas { get; set; }
}
