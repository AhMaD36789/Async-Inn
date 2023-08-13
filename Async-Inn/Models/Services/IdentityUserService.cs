using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    /// <summary>
    /// Represents a service for identity user operations.
    /// </summary>
    public class IdentityUserService : IUser
    {
        private JwtTokenService _tokenService;
        private UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        public IdentityUserService(UserManager<User> userManager, JwtTokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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
            var result = await _userManager.CreateAsync(user, registerDTO.password);

            if (result.Succeeded)
            {
                _userManager.AddToRolesAsync(user, registerDTO.Roles);

                return new UserDTO()
                {
                    ID = user.Id,
                    Username = user.UserName,
                    Token = await _tokenService.GetToken(user, System.TimeSpan.FromMinutes(45)),
                    Roles = await _userManager.GetRolesAsync(user)
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
            var user = await _userManager.FindByNameAsync(username);
            var validPassword = await _userManager.CheckPasswordAsync(user, password);
            if (validPassword)
                return new UserDTO()
                {
                    ID = user.Id,
                    Username = user.UserName,
                    Token = await _tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                    Roles = await _userManager.GetRolesAsync(user)
                };
            return null;
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return new UserDTO
            {
                ID = user.Id,
                Username = user.UserName,
                Token = await _tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
    }
}
