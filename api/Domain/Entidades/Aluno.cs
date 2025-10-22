using System;
using System.Collections.Generic;

namespace Api.Domain.Entidades;

public class Aluno : EntidadeBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }

    public IEnumerable<Turma> Turmas { get; set; }
    public IEnumerable<Matricula> Matriculas { get; set; }
}
