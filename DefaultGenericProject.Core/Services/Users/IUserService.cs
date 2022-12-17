using DefaultGenericProject.Core.DTOs.Responses;
using DefaultGenericProject.Core.DTOs.Users;
using DefaultGenericProject.Core.Models.Users;
using System;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Services.Users
{
    public interface IUserService : IGenericService<User>
    {
        Task<Response<UserDTO>> GetUserByNameAsync(string userName);
        Task<Response<UserDTO>> GetUserByEmailAsync(string userName);
        Task<Response<NoDataDTO>> UpdateUserAsync(UpdateUserDTO updateUserDTO);
        Task<Response<NoDataDTO>> DeleteUserAsync(Guid id);
        Task<Response<NoDataDTO>> IsUnique(string username, string email);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsUsernameUnique(string username);
    }
}