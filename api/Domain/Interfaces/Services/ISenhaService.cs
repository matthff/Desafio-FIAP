using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Services;

public interface ISenhaService<T>
    where T : EntidadeBaseIdentidade
{
    string HashSenha(T entidade, string password);

    bool VerificarSenha(T entidade, string senhaInformada);
}

