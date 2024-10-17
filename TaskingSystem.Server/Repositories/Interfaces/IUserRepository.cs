using TaskingSystem.Server.Entities;
using TaskingSystem.Server.Entities.Request;
using TaskingSystem.Server.Models;

namespace TaskingSystem.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Method to save data user in the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User Model</returns>
        Task<object> Save(RegisterUserRequest request);

        /// <summary>
        /// Method to update data user in the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User Model</returns>
        Task<object> UpdateUser(UpdateUserRequest request);

        /// <summary>
        /// Method to delete user in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User Model</returns>
        Task<object> Delete(int id);
        Task<object> GetUserByEmailAndPassWord(UserCredential request);
        Task<object> GetRoleUser(string userName);

    }
}
