using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    /// <summary>
    /// Represents a service for identity user operations.
    /// </summary>
    public class IdentityUserService : IUser
    {
        private UserManager<User> _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        public IdentityUserService(UserManager<User> userManager)
        {
            _manager = userManager;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDTO">The registration data.</param>
        /// <param name="ModelState">The model state dictionary for validation errors.</param>
        /// <returns>The registered user's data.</returns>
        public async Task<UserDTO> Register(RegisterDTO registerDTO, ModelStateDictionary ModelState)
        {
            var user = new User()
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber
            };
            var result = await _manager.CreateAsync(user, registerDTO.password);

            if (result.Succeeded)
            {
                return new UserDTO()
                {
                    ID = user.Id,
                    Username = user.UserName
                };
            }

            foreach (var error in result.Errors)
            {
                var errorkey = error.Code.Contains("Password") ? nameof(registerDTO.password) :
                    error.Code.Contains("Email") ? nameof(registerDTO.Email) :
                    error.Code.Contains("Username") ? nameof(registerDTO.Username) :
                    "";
                ModelState.AddModelError(errorkey, error.Description);
            }

            return null;
        }

        /// <summary>
        /// Authenticates a user using their username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The authenticated user's data if successful, otherwise null.</returns>
        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await _manager.FindByNameAsync(username);
            var validPassword = await _manager.CheckPasswordAsync(user, password);
            if (validPassword)
                return new UserDTO() { ID = user.Id, Username = user.UserName };
            return null;
        }
    }
}
