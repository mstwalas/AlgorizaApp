using CoreLayer.Model;
using Microsoft.EntityFrameworkCore;
using RepositryLayer;
using RepositryLayer.Interfaces;
using ServicesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using RepositryLayer;
using CoreLayer.Model;
using Microsoft.Extensions.Configuration;

namespace ServicesLayer
{
    public class IdentityService : Repositry<Identity>, IIdentityService
    {
        private readonly IConfiguration configuration;
        public IdentityService(DbContext context) : base(context)
        {
        }

        public void AddUser(Identity User)
        {
            throw new NotImplementedException();
        }

        public string CreateToken(Identity UserIdentity)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,UserIdentity.Username)
            };
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MostafaElsaeed1544$4$MostafaElsaeed1544$4$"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
        public bool verifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return ComputedHash.SequenceEqual(PasswordHash);
            }
        }
    }
}
