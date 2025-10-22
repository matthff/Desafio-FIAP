namespace Api.Domain.Entidades;

public class Administrador : EntidadeBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; }
}
