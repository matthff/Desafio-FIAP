using System;

namespace Api.Domain.DTO.Aluno;

public class AlunoDto : BaseDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
}
