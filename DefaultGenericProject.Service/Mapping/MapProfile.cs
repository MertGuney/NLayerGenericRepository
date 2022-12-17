using AutoMapper;
using DefaultGenericProject.Core.DTOs;
using DefaultGenericProject.Core.DTOs.Logins;
using DefaultGenericProject.Core.DTOs.Roles;
using DefaultGenericProject.Core.DTOs.Users;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Models.Users;

namespace DefaultGenericProject.Service.Mapping
{
    internal class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            #region Users
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<User, LoginResponseDTO>().ReverseMap();

            CreateMap<UserDTO, CreateUserDTO>().ReverseMap();
            CreateMap<UserDTO, UpdateUserDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            #endregion
            #region Auth
            CreateMap<User, RegisterDTO>().ReverseMap();
            #endregion
        }
    }
}