using RegistrationAPI.DTO;
using RegistrationAPI.Models;

namespace RegistrationAPI.Repositories
{
    public interface IUserRepository
    {
        Task<ApiResponse<UserDTO>> RegisterAsync(UserDTO userDTO);
        Task<ApiResponse<string>> LoginAsync(LoginDTO loginDTO);
        Task<ApiResponse<User>> GetProfileAsync(string phone);
    }
}
