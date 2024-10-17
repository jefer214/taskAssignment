namespace TaskingSystem.Server
{
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using TaskingSystem.Server.Models;
    using TaskingSystem.Server.Repositories.Interfaces;

    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public TokenManager(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> Authenticate(string userName, string password)
        {
            Role? role = new();
            var roles = await _userRepository.GetRoleUser(userName);

            if (roles.GetType() == typeof(Role))
            {
                role = roles as Role;
            }

            string? key = _configuration.GetValue<string>("JwtConfig:Key");
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(
                 [
                 new Claim(ClaimTypes.NameIdentifier, userName),
                 new Claim(ClaimTypes.Role, role.NameRol),
                 ]),
                Audience = "TestAudience",
                Issuer = "TestIssuer",
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(
                                      new SymmetricSecurityKey(keyBytes),
                                      SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public JwtSecurityToken? ValidateToken(string token)
        {
            if (token is null)
            {
                return null;
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtConfig:Key"));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtConfig:ValidAudience"],
                    ValidIssuer = _configuration["JwtConfig:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // return user id from JWT token if validation successful
                return jwtToken;
            }
            catch (SecurityTokenExpiredException ex)
            {
                throw new SecurityTokenExpiredException("Token has expired", ex);
            }
        }
    }
}
