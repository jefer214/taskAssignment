using TaskingSystem.Server.Repositories.Interfaces;

namespace TaskingSystem.Server.Repositories
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TaskingSystem.Server.Entities;
    using TaskingSystem.Server.Entities.Request;
    using TaskingSystem.Server.Models;

    public class UserRepository : IUserRepository
    {
        private readonly TaskAssignmentContext _context;

        public UserRepository(TaskAssignmentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to save data user in the data base
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User Model</returns>
        public async Task<object> Save(RegisterUserRequest request)
        {
            try
            {
                Role? validateRol = _context.Roles.FirstOrDefault(r => r.Id == request.RoleId);

                if (validateRol is null)
                {
                    return new NotFoundObjectResult("Rol no found");
                }

                User user = new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password,
                    RolId = request.RoleId,
                };

                await _context.Users.AddAsync(user).ConfigureAwait(false);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to update data user in the data base
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User Model</returns>
        public async Task<object> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                Role? role = _context.Roles.FirstOrDefault(r => r.Id == request.RoleId);
                User? user = _context.Users.FirstOrDefault(r => r.Id == request.UserId);

                if (user is null
                    || role is null)
                {
                    string field = user is null ? "User" : "Role";
                    return new NotFoundObjectResult($"{field} no found");
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.Password = request.Password;
                user.RolId = request.RoleId;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Method to delete user in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User Model</returns>
        public async Task<object> Delete(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);

                if (user is null)
                {
                    return new NotFoundObjectResult("User no found");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<object> GetUserByEmailAndPassWord(UserCredential request)
        {
            try
            {
                User? user = _context.Users.FirstOrDefault(u => u.Email == request.Email
                                                && u.Password.Equals(request.Password));

                if (user is null)
                {
                    return new NotFoundObjectResult("User no found");
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public async Task<object> GetRoleUser(string userName)
        {
            try
            {
                var user = _context.Users.Include(u => u.Rol)
                                    .FirstOrDefault(u => u.Email == userName);

                if (user is null)
                {
                    return new NotFoundObjectResult("Rol no found");
                }

                return user.Rol;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
