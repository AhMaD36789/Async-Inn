using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Async_Inn.Models.Services
{
    public class JwtTokenService
    {
        private IConfiguration _configuration;
        private SignInManager<User> _signInManager;

        public JwtTokenService(IConfiguration config, SignInManager<User> manager)
        {
            _configuration = config;
            _signInManager = manager;
        }

        public static TokenValidationParameters GetValidationParameter(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null)
            {
                throw new InvalidOperationException("JWT : Secret key doesn't exist");
            }

            var byteSecret = Encoding.UTF8.GetBytes(secret);

            return new SymmetricSecurityKey(byteSecret);
        }

        public async Task<string> GetToken(User user, TimeSpan expireIn)
        {
            var principle = await _signInManager.CreateUserPrincipalAsync(user);
            if (principle == null)
                return null;
            var signingKey = GetSecurityKey(_configuration);
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow + expireIn,
                signingCredentials: new SigningCredentials(
                    signingKey, SecurityAlgorithms.HmacSha256),
                    claims: principle.Claims
                    );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
