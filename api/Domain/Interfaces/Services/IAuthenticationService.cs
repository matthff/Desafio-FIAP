using System.Collections.Generic;
using System.Security.Claims;

namespace Api.Domain.Interfaces.Services;

public interface IAuthenticationService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

