using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoBolao.Service.API.Services
{
    public class TokenService
    {
        public static string GerarJsonWebToken(string claimName, string claimValue)
        {
            var geradorToken = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes("@GOBOLAO@API@CHAVE@");
            var descricaoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(claimName, claimValue) }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(30)
            };
            var token = geradorToken.CreateToken(descricaoToken);
            return geradorToken.WriteToken(token);
        }
    }
}
