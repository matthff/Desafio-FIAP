namespace Api.Domain.DTO.Token;

public class TokenDto
{
    public bool Authenticated { get; set; }
    public string CreateDate { get; set; }
    public string ExpirationDate { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

