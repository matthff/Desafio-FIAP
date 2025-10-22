namespace Api.Domain.DTO.Turma;

public class TurmaDto : BaseDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeDeAlunos { get; set; }
}
