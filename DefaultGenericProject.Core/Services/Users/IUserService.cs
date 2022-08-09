using DefaultGenericProject.Core.Dtos.Responses;
using DefaultGenericProject.Core.DTOs.Users;
using DefaultGenericProject.Core.Models.Users;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Services.Users
{
    public interface IUserService : IGenericService<User, AppUserDto>
    {
        Task<Response<AppUserDto>> GetUserByNameAsync(string userName);
        Task<Response<AppUserDto>> GetUserByEmailAsync(string userName);
        Task<Response<NoDataDto>> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<Response<NoDataDto>> DeleteUserAsync(string id);
        Task<Response<NoDataDto>> IsUnique(string username, string email);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsUsernameUnique(string username);
    }
}