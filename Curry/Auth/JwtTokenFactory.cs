using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Curry.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Curry.Auth
{
    public class JwtTokenFactory : ITokenFactory<JwtSecurityToken>
    {
        private readonly IConfiguration _config;

        public JwtTokenFactory(IConfiguration config)
        {
            _config = config;
        }

        public JwtSecurityToken GenerateToken(User user)
        {
            var claims = new List<Claim>();
            {
                var sub = new Claim(JwtRegisteredClaimNames.Sub, user.Username);
                var jti = new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString());
            };
            if (user.UserRoles != null)
            {
                claims.AddRange(user.UserRoles.Select(role => new Claim(ClaimTypes.Role, role.Role.Description)));
            }

            var tesdti = 1;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(311),
                signingCredentials: credentials);
            return token;
        }
    }
}