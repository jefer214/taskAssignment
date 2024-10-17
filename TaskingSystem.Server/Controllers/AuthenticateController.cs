namespace TaskingSystem.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskingSystem.Server.Entities;
    using TaskingSystem.Server.Models;
    using TaskingSystem.Server.Repositories.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        private readonly IUserRepository _userRepository;

        public AuthenticateController(IUserRepository userRepository,
            ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _tokenManager = tokenManager;
        }

        /// <summary>
        /// Method to obtain the token
        /// </summary>
        /// <param name="userCredential"></param>
        /// <returns>Token</returns>
        [AllowAnonymous]
        [HttpPost, Route("Authenticate")]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredential userCredential)
        {
            object validateUser = await _userRepository.GetUserByEmailAndPassWord(userCredential);

            if (validateUser.GetType() == typeof(User))
            {
                var token = _tokenManager.Authenticate(userCredential.Email, userCredential.Password);
                return Ok(token);
            }
            
            return Unauthorized();
        }
    }
}
