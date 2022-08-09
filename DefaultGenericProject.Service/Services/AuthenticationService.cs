using DefaultGenericProject.Core.Configuration;
using DefaultGenericProject.Core.Dtos.Responses;
using DefaultGenericProject.Core.DTOs.Logins;
using DefaultGenericProject.Core.DTOs.Tokens;
using DefaultGenericProject.Core.Models.Users;
using DefaultGenericProject.Core.Repositories;
using DefaultGenericProject.Core.Repositories.Users;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.UnitOfWorks;
using DefaultGenericProject.Service.Mapping;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;

        public AuthenticationService(IOptions<List<Client>> clientOptions, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, ITokenService tokenService, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)
        {
            _clients = clientOptions.Value;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            return await _userRepository.Where(x => x.Email == email && x.Password == PasswordHash(password)).AnyAsync();
        }

        public async Task<Response<TokenDTO>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userRepository.Where(x => x.Email == loginDto.Email).FirstOrDefaultAsync();
            if (user == null) return Response<TokenDTO>.Fail("Email veya Şifre yanlış.", 400, true);

            if (!await CheckPasswordAsync(loginDto.Email, loginDto.Password))
            {
                return Response<TokenDTO>.Fail("Email veya Şifre yanlış", 400, true);
            }
            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                await _userRefreshTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommmitAsync();

            return Response<TokenDTO>.Success(token, 200);
        }

        public Response<ClientTokenDTO> CreateTokenByClientAsync(ClientLoginDTO clientLoginDTO)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDTO.ClientId && x.Secret == clientLoginDTO.ClientSecret);
            if (client == null)
            {
                return Response<ClientTokenDTO>.Fail("ClientId or ClientSecret not found", 404, true);
            }
            var token = _tokenService.CreateTokenByClient(client);
            return Response<ClientTokenDTO>.Success(token, 200);
        }

        public async Task<Response<TokenDTO>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return Response<TokenDTO>.Fail("Refresh token not found", 404, true);
            }
            var user = await _userRepository.GetByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                return Response<TokenDTO>.Fail("User not found", 404, true);
            }
            var tokenDTO = _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDTO.RefreshToken;
            existRefreshToken.Expiration = tokenDTO.RefreshTokenExpiration;
            await _unitOfWork.CommmitAsync();
            return Response<TokenDTO>.Success(tokenDTO, 200);
        }

        public async Task<Response<LoginResultDTO>> Login(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userRepository.Where(x => x.Email == loginDto.Email).FirstOrDefaultAsync();
            if (user == null) return Response<LoginResultDTO>.Fail("Email veya Şifre yanlış.", 400, true);

            if (!await CheckPasswordAsync(loginDto.Email, loginDto.Password))
            {
                return Response<LoginResultDTO>.Fail("Email veya Şifre yanlış", 400, true);
            }
            var userMap = ObjectMapper.Mapper.Map<LoginResultDTO>(user);
            return Response<LoginResultDTO>.Success(userMap, 200);
        }

        public string PasswordHash(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)
            );
        }

        public async Task<Response<LoginResultDTO>> Register(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return Response<LoginResultDTO>.Fail("Model boş gönderilemez.", 404, true);
            }
            registerDTO.Password = PasswordHash(registerDTO.Password);
            var userMap = ObjectMapper.Mapper.Map<User>(registerDTO);
            userMap.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            await _userRepository.AddAsync(userMap);
            await _unitOfWork.CommmitAsync();
            var userCreatedMap = ObjectMapper.Mapper.Map<LoginResultDTO>(userMap);
            return Response<LoginResultDTO>.Success(userCreatedMap, 200);
        }

        public async Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("Refresh token not found", 404, true);
            }
            _userRefreshTokenRepository.Remove(existRefreshToken);
            await _unitOfWork.CommmitAsync();
            return Response<NoDataDto>.Success(200);
        }
    }
}