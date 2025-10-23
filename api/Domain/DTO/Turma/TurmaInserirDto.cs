using System.ComponentModel.DataAnnotations;

using E = Api.Domain.Entidades;
namespace Api.Domain.DTO.Turma;

public class TurmaInserirDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(
        E.Turma.TamanhoMaximoNome,
        MinimumLength = E.Turma.TamanhoMinimoNome)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(
        E.Turma.TamanhoMaximoDescricao,
        MinimumLength = E.Turma.TamanhoMinimoDescricao)]
    public string Descricao { get; set; }
}
