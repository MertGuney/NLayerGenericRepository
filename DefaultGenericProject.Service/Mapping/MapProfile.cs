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
            CreateMap<AppUserDto, User>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<LoginResultDTO, User>().ReverseMap();

            CreateMap<AppUserDto, CreateUserDto>().ReverseMap();
            CreateMap<AppUserDto, UpdateUserDto>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            #endregion
            #region Auth
            CreateMap<RegisterDTO, User>().ReverseMap();
            #endregion
        }
    }
}