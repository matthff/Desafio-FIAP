using Api.Domain.Entidades;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

public class SenhaService<T> : ISenhaService<T>
    where T : EntidadeBaseIdentidade
{
    private readonly PasswordHasher<T> _passwordHasher;

    public SenhaService()
    {
        _passwordHasher = new PasswordHasher<T>();
    }

    public string HashSenha(T entidade, string password)
    {
        return _passwordHasher.HashPassword(entidade, password);
    }

    public bool VerificarSenha(T entidade, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(entidade, entidade.SenhaHash, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}
