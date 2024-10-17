namespace TaskingSystem.Server.AuthorizationFilter
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;
    using TaskingSystem.Server.Models;
    using TaskingSystem.Server.Repositories.Interfaces;

    public class AuthorizationFilter : ControllerBase, IAuthorizationFilter
    {
        private readonly TaskAssignmentContext _context;
        private readonly ITokenManager _tokenManager;
        private readonly string _method;
        public AuthorizationFilter(
            string method,
            TaskAssignmentContext context,
            ITokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
            _method = method;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = StatusCode(StatusCodes.Status400BadRequest, new { Title = "BadRequest", Status = 400, Message = "Empy Token" });
                return;
            }

            var jwtToken = _tokenManager.ValidateToken(token);

            if (jwtToken is null)
            {
                context.Result = StatusCode(StatusCodes.Status401Unauthorized, new { Title = "Unauthorized", Status = 401, Message = "Invalid authorizate. Token not valid try to login again." });
                return;
            }
            
            Claim? roles = jwtToken.Claims.FirstOrDefault(x => x.Type == "role");

            var query = from permission in _context.Permissions
                        join roleInPermissions in _context.RoleInPermissions
                        on permission.Id equals roleInPermissions.PermissionId
                        join role in _context.Roles
                        on roleInPermissions.RolId equals role.Id
                        where
                        role.NameRol == roles.Value
                        select new
                        {
                            permission.NamePermission,
                        };

            if (!query.Any(p => p.NamePermission.Equals(_method)))
            {
                context.Result = StatusCode(StatusCodes.Status401Unauthorized, new { Title = "Unauthorized", Status = 401, Message = "You do not have access to this method." });
                return;
            }

        }
    }
}
