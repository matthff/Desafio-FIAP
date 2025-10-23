using System;
using System.ComponentModel.DataAnnotations;
using E = Api.Domain.Entidades;

namespace Api.Domain.DTO.Administrador;

public class AdministradorInserirDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(E.Administrador.TamanhoMaximoNome, MinimumLength = E.Administrador.TamanhoMinimoNome)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [MaxLength(E.Administrador.TamanhoMaximoEmail)]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(E.Administrador.TamanhoMinimoSenha)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "A senha deve conter ao menos uma letra maiúscula, uma minúscula, um número e um símbolo.")]
    public string Senha { get; set; }
}
