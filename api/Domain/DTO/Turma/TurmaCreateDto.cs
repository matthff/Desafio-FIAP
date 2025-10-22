using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTO.Turma;

public class TurmaCreateDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; }
}
