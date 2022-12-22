using DefaultGenericProject.Core.DTOs.Paging;
using DefaultGenericProject.Core.DTOs.Users;
using DefaultGenericProject.Core.Services.Users;
using DefaultGenericProject.Core.StringInfos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DefaultGenericProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleInfo.Admin)]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _userService.GetAllAsync());
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult GetAll([FromQuery] PagingParamaterDTO pagingParamaterDTO)
        {
            return ActionResultInstance(_userService.GetAll<UserDTO>(pagingParamaterDTO, x => x.Email.Contains(pagingParamaterDTO.Search)));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return ActionResultInstance(await _userService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDTO updateUserDTO)
        {
            return ActionResultInstance(await _userService.UpdateUserAsync(updateUserDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return ActionResultInstance(await _userService.DeleteUserAsync(id));
        }
    }
}
