namespace TaskingSystem.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using TaskingSystem.Server.AuthorizationFilter;
    using TaskingSystem.Server.Entities.Request;
    using TaskingSystem.Server.Models;
    using TaskingSystem.Server.Repositories.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User Model</returns>
        [AuthorizationFilter(nameof(CreateUser))]
        [HttpPost, Route("Register")]
        public async Task<IActionResult> CreateUser([FromHeader] RegisterUserRequest request)
        {
            var objectResult = await _userRepository.Save(request);

            if (objectResult.GetType() == typeof(User))
            {
                return Ok(objectResult);
            }
            
            return BadRequest();
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <returns>User Model, ErrorResponses ViewModel</returns>
        [AuthorizationFilter(nameof(UpdateUser))]
        [HttpPut, Route("UpdateTask")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromHeader] UpdateUserRequest request)
        {
            object objectResult = await _userRepository.UpdateUser(request);

            if (objectResult.GetType() == typeof(User))
            {
                return Ok(objectResult);
            }
            return BadRequest(objectResult);
        }

        /// <summary>
        /// Delete User by Id
        /// </summary>
        /// <returns> User Model</returns>
        [AuthorizationFilter(nameof(DeleteUser))]
        [HttpDelete]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            object objectResult = await _userRepository.Delete(id);

            if (objectResult.GetType() == typeof(User))
            {
                return Ok(objectResult);
            }
            return BadRequest(objectResult);
        }
    }
}
