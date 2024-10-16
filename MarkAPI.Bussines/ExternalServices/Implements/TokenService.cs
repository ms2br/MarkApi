using MarkAPI.Bussines.Dtos.TokenDtos;
using MarkAPI.Bussines.ExternalServices.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.ExternalServices.Implements
{
    public class TokenService : ITokenService
    {
        JWT jwt { get; }

        public TokenService(IOptionsMonitor<JWT> option)
        {
            jwt = option.CurrentValue;
        }
        public async Task<TokenDto> CreateAccessTokenAsync(string userId)
        {
            TokenDto tokenDto = new();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            tokenDto.Expiration = DateTime.UtcNow.AddHours(Convert.ToInt32(jwt.LifeSpan));
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

            JwtSecurityToken jwtToken = new
                (
                audience: jwt.Audience,
                issuer: jwt.Issuer,
                claims: claims,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                expires: tokenDto.Expiration
                );
            JwtSecurityTokenHandler jwtHandler = new();
            tokenDto.AccessToken = jwtHandler.WriteToken(jwtToken);
            return tokenDto;
        }
    }
}
