#nullable enable

using System;

namespace Api.Domain.Entidades;

public class Administrador : EntidadeBaseIdentidade
{
    public const int TamanhoMinimoNome = 3;
    public const int TamanhoMaximoNome = 100;
    public const int TamanhoMaximoEmail = 200;
    public const int TamanhoMinimoSenha = 8;

    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpireTime { get; private set; }

    public void DefinirRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public void DefinirRefreshTokenExpireTime(DateTime expireTime)
    {
        RefreshTokenExpireTime = expireTime;
    }

    public void RevogarRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpireTime = null;
    }
}
