using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.TokenService.Models;

namespace Umio.API.TokenService.Services
{
    internal class TokenGenerator : ITokenService
    {
        private readonly ConfiguracoesJwt _configuracoesJwt;
        public TokenGenerator(ConfiguracoesJwt configuracoesJwt)
        {
            _configuracoesJwt = configuracoesJwt;
        }
        public string GerarToken(string email, string senha)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracoesJwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuracoesJwt.Issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
