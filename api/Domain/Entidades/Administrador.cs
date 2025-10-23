namespace Api.Domain.Entidades;

public class Administrador : EntidadeBaseIdentidade
{
    public const int TamanhoMinimoNome = 3;
    public const int TamanhoMaximoNome = 100;
    public const int TamanhoMaximoEmail = 200;
    public const int TamanhoMinimoSenha = 8;
}
