using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Curry.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Curry.Auth
{
    public class JwtTokenFactory : ITokenFactory<JwtSecurityToken>
    {
        private readonly IConfiguration config;
        public JwtTokenFactory(IConfiguration config)
        {
            this.config = config;
        }
        public JwtSecurityToken GenerateToken(User user)
        {
            var claims = new[]
                   {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Tokens:Issuer"],
            config["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: creds);
            return token;
        }
    }
}
