using System;

namespace Api.Domain.Entidades;

public abstract class EntidadeBaseIdentidade : EntidadeBase
{
    public string Email { get; set; }
    public string SenhaHash { get; private set; }

    public void DefinirSenha(string senhaHash)
    {
        SenhaHash = senhaHash;
    }
}
