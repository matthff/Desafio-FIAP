using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTO.Turma;

public class TurmaCreateDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MinLength(10, ErrorMessage = "A descrição deve ter no mínimo 10 caracteres.")]
    [MaxLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
    public string Descricao { get; set; }
}
