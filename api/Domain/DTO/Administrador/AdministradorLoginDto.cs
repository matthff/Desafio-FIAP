using System.ComponentModel.DataAnnotations;
using E = Api.Domain.Entidades;

namespace Api.Domain.DTO.Administrador;

public class AdministradorLoginDto
{
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [MaxLength(E.Administrador.TamanhoMaximoEmail)]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Senha { get; set; }
}
