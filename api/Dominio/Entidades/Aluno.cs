using System;

namespace Api.Dominio.Entidades;

public class Aluno : EntidadeBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
}
