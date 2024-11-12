using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Token;
using BookShelfter.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BookShelfter.Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Application.DTOs.Token? CreateAccessToken(int accessTokenLifetime, AppUser user, IList<string> roles)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.UserName)
                    };


                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));

                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(accessTokenLifetime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature),
                    Audience = _configuration["Jwt:Audience"],
                    Issuer = _configuration["Jwt:Issuer"]

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var generatedToken = new Application.DTOs.Token
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    Expiration = tokenDescriptor.Expires.Value
                };

                _logger.LogInformation($"Token created with expiration time: {generatedToken.Expiration}");


                return generatedToken;


            }
            catch (Exception ex)
            {
              

                throw new Exception("An error occurred during token creation", ex);
            }
        }

        public string CreateRefreshToken()
        {

            return Guid.NewGuid().ToString();

        }

    }
}
