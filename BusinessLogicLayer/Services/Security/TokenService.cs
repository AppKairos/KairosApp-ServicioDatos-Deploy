using ImprentaAPI.BusinessLogicLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _ssKey;
        public TokenService(IConfiguration configuration)
        {
            _ssKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token"]));

        }
        public string CreateToken(UsuarioModel usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,usuario.Email.ToString())
            };
            var credenciales = new SigningCredentials(_ssKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
