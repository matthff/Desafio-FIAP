using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Services;

public interface ISenhaService<T>
    where T : EntidadeBaseIdentidade
{
    string DefinirHashDaSenha(T entidade, string password);

    bool ValidarSenha(T entidade, string senhaInformada);
}

