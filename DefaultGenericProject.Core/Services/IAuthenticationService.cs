using DefaultGenericProject.Core.DTOs.Responses;
using DefaultGenericProject.Core.DTOs.Logins;
using DefaultGenericProject.Core.DTOs.Tokens;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Token alarak login olma işlemi
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        Task<Response<TokenDTO>> CreateTokenAsync(LoginDTO loginDTO);
        /// <summary>
        /// Kullanıcının refresh tokenı ile yeni access token oluşturma işlemi
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<Response<TokenDTO>> CreateTokenByRefreshToken(string refreshToken);
        /// <summary>
        /// Client bilgileri için token oluşturma işlemi
        /// </summary>
        /// <param name="clientLoginDTO"></param>
        /// <returns></returns>
        Response<ClientTokenDTO> CreateTokenByClientAsync(ClientLoginDTO clientLoginDTO);
        /// <summary>
        /// Kullanıcının refresh tokenını silme işlemi
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<Response<NoDataDTO>> RevokeRefreshToken(string refreshToken);
        /// <summary>
        /// Giriş yapma işlemi
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        Task<Response<LoginResponseDTO>> Login(LoginDTO loginDTO);
        /// <summary>
        /// Kullanıcı kayıt işlemi
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        Task<Response<LoginResponseDTO>> Register(RegisterDTO registerDTO);
        /// <summary>
        /// Şifreyi kontrol etme işlemi
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CheckPasswordAsync(string email, string password);
        /// <summary>
        /// Şifre hashleme işlemi
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string PasswordHash(string password);
    }
}