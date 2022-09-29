using DefaultGenericProject.Core.DTOs.Responses;
using DefaultGenericProject.Core.DTOs.Users;
using DefaultGenericProject.Core.Enums;
using DefaultGenericProject.Core.Models.Users;
using DefaultGenericProject.Core.Repositories;
using DefaultGenericProject.Core.Repositories.Users;
using DefaultGenericProject.Core.Services.Users;
using DefaultGenericProject.Core.UnitOfWorks;
using DefaultGenericProject.Service.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<NoDataDTO>> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            var user = await _userRepository.GetByIdAsync(updateUserDTO.Id);
            if (user == null)
            {
                Response<NoDataDTO>.Fail("Kullanıcı bulunamadı.", 404, true);
            }
            updateUserDTO.FullName ??= (updateUserDTO.Name + " " + updateUserDTO.Surname).ToUpper();
            var userMap = ObjectMapper.Mapper.Map<User>(updateUserDTO);
            userMap.Password = user.Password;
            userMap.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            _userRepository.Update(userMap);
            _unitOfWork.Commit();
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return Response<NoDataDTO>.Fail("Kullanıcı bulunamadı.", 404, true);
            }
            user.Status = DataStatus.Inactive;

            _userRepository.Update(ObjectMapper.Mapper.Map<User>(user));
            _unitOfWork.Commit();
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<AppUserDTO>> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return Response<AppUserDTO>.Fail("Email bulunamadı.", 404, true);
            }
            var userMap = ObjectMapper.Mapper.Map<AppUserDTO>(user);
            return Response<AppUserDTO>.Success(userMap, 200);
        }

        public async Task<Response<AppUserDTO>> GetUserByNameAsync(string username)
        {
            var user = await _userRepository.Where(x => x.Username == username).FirstOrDefaultAsync();
            if (user == null)
            {
                return Response<AppUserDTO>.Fail("Kullanıcı adı bulunamadı.", 404, true);
            }
            return Response<AppUserDTO>.Success(ObjectMapper.Mapper.Map<AppUserDTO>(user), 200);
        }

        public async Task<Response<NoDataDTO>> IsUnique(string username, string email)
        {
            var isUsernameUnique = await GetUserByNameAsync(username);
            if (isUsernameUnique != null)
            {
                return Response<NoDataDTO>.Fail("Kullanıcı adı zaten mevcut.", 404, true);
            }
            var isEmailUnique = await GetUserByEmailAsync(email);
            if (isEmailUnique != null)
            {
                return Response<NoDataDTO>.Fail("Email zaten mevcut.", 404, true);
            }
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            var isEmailUnique = await GetUserByEmailAsync(email);
            if (isEmailUnique != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsUsernameUnique(string username)
        {
            var isUsernameUnique = await GetUserByNameAsync(username);
            if (isUsernameUnique != null)
            {
                return false;
            }
            return true;
        }
    }
}