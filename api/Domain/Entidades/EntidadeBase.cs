using System;

namespace Api.Domain.Entidades;

public abstract class EntidadeBase : EntidadeId
{
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAtualizacao { get; set; }
}
