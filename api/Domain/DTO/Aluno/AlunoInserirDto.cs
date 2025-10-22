using System;
using System.ComponentModel.DataAnnotations;
using E = Api.Domain.Entidades;

namespace Api.Domain.DTO.Aluno;

public class AlunoInserirDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(E.Aluno.TamanhoMaximoNome, MinimumLength = E.Aluno.TamanhoMinimoNome)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [MaxLength(E.Aluno.TamanhoMaximoEmail)]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(E.Aluno.TamanhoMinimoSenha)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "A senha deve conter ao menos uma letra maiúscula, uma minúscula, um número e um símbolo.")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos numéricos.")]
    [CpfValido(ErrorMessage = "CPF inválido.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [DataType(DataType.Date)]
    [DataNascimentoValida(ErrorMessage = "Data de nascimento inválida.")]
    public DateTime DataNascimento { get; set; }
}
