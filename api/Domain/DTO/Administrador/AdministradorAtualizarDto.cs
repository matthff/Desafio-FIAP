using System.ComponentModel.DataAnnotations;
using E = Api.Domain.Entidades;

namespace Api.Domain.DTO.Administrador;

public class AdministradorAtualizarDto
{
    [Required(ErrorMessage = "O Id é obrigatório.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(E.Administrador.TamanhoMaximoNome, MinimumLength = E.Administrador.TamanhoMinimoNome)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [MaxLength(E.Administrador.TamanhoMaximoEmail)]
    public string Email { get; set; }
}
