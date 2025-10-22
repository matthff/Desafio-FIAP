using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTO.Aluno;

public class AlunoAtualizarDto : AlunoInserirDto
{
    [Required(ErrorMessage = "O Id é obrigatório.")]
    public int Id { get; set; }
}
