using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Vladrega.StreamAlerts.Authentication;

internal static class JwtOptions
{
    internal const string Issuer = "Vladrega";

    internal static string GetJwt(string userName)
    {
        var utcNow = DateTime.UtcNow;
        var expireTime = utcNow.AddMonths(6);

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userName)
        });
        
        var jwt = new JwtSecurityToken(
            issuer: Issuer, 
            notBefore: DateTime.UtcNow, 
            expires: expireTime, 
            claims: identity.Claims,
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    internal static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OmegaTopSecret1337"));
    }
}