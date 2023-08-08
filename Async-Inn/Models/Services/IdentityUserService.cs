namespace Async_Inn.Models.Services
{
    using Async_Inn.Models.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Threading.Tasks;

    public class IdentityUserService : IUser
    {
        private UserManager<User> _manager;

        public IdentityUserService(UserManager<User> userManager)
        {
            _manager = userManager;
        }


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
