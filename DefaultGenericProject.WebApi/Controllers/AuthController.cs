using DefaultGenericProject.Core.DTOs.Logins;
using DefaultGenericProject.Core.DTOs.Tokens;
using DefaultGenericProject.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DefaultGenericProject.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return ActionResultInstance(await _authenticationService.CreateTokenAsync(loginDto));
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            return ActionResultInstance(await _authenticationService.Register(registerDTO));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            return ActionResultInstance(await _authenticationService.CreateTokenByRefreshToken(refreshTokenDTO.Token));
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDTO clientLoginDTO)
        {
            return ActionResultInstance(_authenticationService.CreateTokenByClientAsync(clientLoginDTO));
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            return ActionResultInstance(await _authenticationService.RevokeRefreshToken(refreshTokenDTO.Token));
        }
    }
}
