using System;
using System.Collections.Generic;

namespace Api.Domain.Entidades;

public class Aluno : EntidadeBaseIdentidade
{
    public const int TamanhoMinimoNome = 3;
    public const int TamanhoMaximoNome = 100;
    public const int TamanhoMaximoEmail = 200;
    public const int TamanhoMinimoSenha = 8;

    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }

    public IEnumerable<Turma> Turmas { get; set; }
    public IEnumerable<Matricula> Matriculas { get; set; }
}
