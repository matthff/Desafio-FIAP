using System;

namespace Api.Domain.DTO;

public abstract class BaseDto
{
    public int Id { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAtualizacao { get; set; }
}
