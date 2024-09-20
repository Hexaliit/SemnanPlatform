using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Infrastructure.Repositories
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Generate(User user)
        {

            var claims = new List<Claim>
            {
                new Claim("Id",Convert.ToString(user.Id)),
                new Claim(ClaimTypes.Email, user.Email),
            };

            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }


            var signingcreds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtToken:Key"]!)),
                SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: configuration["JwtToken:Issuer"],
                audience: configuration["JwtToken:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingcreds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
