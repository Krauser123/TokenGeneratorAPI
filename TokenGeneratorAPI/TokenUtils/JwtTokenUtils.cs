using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TokenGeneratorAPI.ConfigurationModels;

namespace TokenGeneratorAPI
{
    internal class JwtTokenUtils
    {
        private readonly IOptions<JwtToken> JwtToken;

        public JwtTokenUtils(IOptions<JwtToken> jwtToken)
        {
            JwtToken = jwtToken;
        }

        public string BuildJWTToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtToken.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = JwtToken.Value.Issuer;
            var audience = JwtToken.Value.Audience;
            var jwtValidity = DateTime.Now.AddMinutes(Convert.ToDouble(JwtToken.Value.TokenExpiry));

            var token = new JwtSecurityToken(issuer,
              audience,
              expires: jwtValidity,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
