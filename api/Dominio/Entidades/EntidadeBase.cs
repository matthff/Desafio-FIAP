using System;

namespace Api.Dominio.Entidades;

public abstract class EntidadeBase
{
    public long Id { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAtualizacao { get; set; }
}
