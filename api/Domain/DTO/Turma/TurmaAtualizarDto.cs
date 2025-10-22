using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTO.Turma;

public class TurmaAtualizarDto : TurmaCreateDto
{
    [Required(ErrorMessage = "O Id é obrigatório.")]
    public int Id { get; set; }
}
