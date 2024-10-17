namespace TaskingSystem.Server.AuthorizationFilter
{
    using Microsoft.AspNetCore.Mvc;

    public class AuthorizationFilterAttribute : TypeFilterAttribute
    {
        public AuthorizationFilterAttribute(string method) : base(typeof(AuthorizationFilter))
        {
            Arguments = [method];
        }
    }
}
