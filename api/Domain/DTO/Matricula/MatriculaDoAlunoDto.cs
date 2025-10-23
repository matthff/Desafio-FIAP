using System.ComponentModel.DataAnnotations;
namespace Api.Domain.DTO.Turma;

public class MatriculaDoAlunoDto
{
    [Required(ErrorMessage = "O identificador do aluno é obrigatório.")]
    public int AlunoId { get; set; }

    [Required(ErrorMessage = "O identificador da turma é obrigatório.")]
    public int TurmaId { get; set; }
}
