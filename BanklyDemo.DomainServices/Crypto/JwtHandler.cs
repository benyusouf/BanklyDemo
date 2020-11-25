using BanklyDemo.Core.Common;
using BanklyDemo.Core.Users.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BanklyDemo.DomainServices.Crypto
{
    internal class JwtHandler : IJwtHandler
    {
        private const int MinutesToExpire = 60;
        private const string tokenKey = "2SDMJ6fOJJ8kZsURfRjIDMJI1ZYqv6ZLHCBlHHNAInI";
        private const string baseUrl = "https://localhost:44327/";
        private const string appName = "BanklyDemo";

        public string CreateAccessToken(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(MinutesToExpire);
            var jwt = CreateSecurityToken(claims, expiry, signingCredentials, issuer: baseUrl, audience: appName);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }

        private JwtSecurityToken CreateSecurityToken(IEnumerable<Claim> claims, DateTime expiry, SigningCredentials credentials, string issuer, string audience)
            => new JwtSecurityToken(claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiry,
                signingCredentials: credentials,
                issuer: issuer,
                audience: audience);
    }
}
