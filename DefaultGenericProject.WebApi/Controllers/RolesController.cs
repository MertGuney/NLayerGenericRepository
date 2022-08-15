using DefaultGenericProject.Core.DTOs.Roles;
using DefaultGenericProject.Core.Models.Users;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.StringInfos;
using DefaultGenericProject.Service.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DefaultGenericProject.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = RoleInfo.Admin)]
    public class RolesController : CustomBaseController
    {
        private readonly IGenericService<Role> _roleService;
        private readonly IGenericService<UserRole> _userRoleService;

        public RolesController(IGenericService<Role> roleService, IGenericService<UserRole> userRoleService)
        {
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDTO roleDTO)
        {
            return ActionResultInstance(await _roleService.AddAsync(ObjectMapper.Mapper.Map<Role>(roleDTO)));
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(UserRoleDTO userRoleDTO)
        {
            return ActionResultInstance(await _userRoleService.AddAsync(ObjectMapper.Mapper.Map<UserRole>(userRoleDTO)));
        }
    }
}
