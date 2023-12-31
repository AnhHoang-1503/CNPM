﻿using CNPM.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CNPM.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public string Generate(User user)
        {
            var claims = new List<Claim>();

            if (user != null && !string.IsNullOrEmpty(user.Username))
            {
                claims.Add(new Claim("user_name", user.Username));
                claims.Add(new Claim("user_id", user.Id.ToString()));
                claims.Add(new Claim("role", user.Role.ToString()));
            }

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, null, DateTime.UtcNow.AddDays(1), signingCredentials);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
