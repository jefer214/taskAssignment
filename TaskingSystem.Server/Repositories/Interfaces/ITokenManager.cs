namespace TaskingSystem.Server.Repositories.Interfaces
{
    using System.IdentityModel.Tokens.Jwt;

    public interface ITokenManager
    {
        Task<string> Authenticate(string userName, string password);

        JwtSecurityToken? ValidateToken(string token);
    }
}
