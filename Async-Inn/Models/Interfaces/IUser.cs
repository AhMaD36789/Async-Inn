using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterDTO registerDTO, ModelStateDictionary ModelState);
        public Task<UserDTO> Authenticate(string username, string password);
    }
}
