using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Defines the contract for the User service.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDTO">The registration information of the new user.</param>
        /// <param name="ModelState">The model state dictionary to validate the registration information.</param>
        /// <returns>A task that represents the asynchronous operation of registering a new user.</returns>
        public Task<UserDTO> Register(RegisterDTO registerDTO, ModelStateDictionary ModelState);

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="username">The username of the user to authenticate.</param>
        /// <param name="password">The password of the user to authenticate.</param>
        /// <returns>A task that represents the asynchronous operation of authenticating a user.</returns>
        public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal principal);
    }
}
