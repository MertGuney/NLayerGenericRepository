﻿using DefaultGenericProject.Core.Configuration;
using DefaultGenericProject.Core.DTOs.Tokens;
using DefaultGenericProject.Core.Models.Users;
using DefaultGenericProject.Core.Repositories;
using DefaultGenericProject.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly List<Client> _clients;
        private readonly CustomTokenOption _customTokenOption;
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;

        public TokenService(IOptions<CustomTokenOption> customTokenOption, IGenericRepository<UserRole> userRoleRepository, IOptions<List<Client>> clients, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)
        {
            _clients = clients.Value;
            _userRoleRepository = userRoleRepository;
            _customTokenOption = customTokenOption.Value;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public TokenDTO CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_customTokenOption.SecurityKey);

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new(
                issuer: _customTokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(user, _customTokenOption.Audience),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDTO = new TokenDTO
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(user.Id),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
            };
            return tokenDTO;
        }

        public ClientTokenDTO CreateTokenByClient(Client client)
        {
            var hasClient = _clients.Where(x => x.Id == client.Id).FirstOrDefault();
            if (hasClient == null) throw new Exception("Client bulunamadı.");

            var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_customTokenOption.SecurityKey);

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new(issuer: _customTokenOption.Issuer, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: GetClaimsByClient(client), signingCredentials: signingCredentials);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            return new ClientTokenDTO { AccessToken = token, AccessTokenExpiration = accessTokenExpiration };
        }

        private IEnumerable<Claim> GetClaims(User user, List<string> audiences)
        {
            var userList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var userRoles = _userRoleRepository.Where(x => x.UserId == user.Id).Include(x => x.Role).ToList();
            userList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x.Role.Name)));
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }

        private static IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString()),
                new Claim(ClaimTypes.Role, client.Id.ToString()),
            };
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return claims;
        }

        private string CreateRefreshToken(Guid userId)
        {
            var refreshToken = _userRefreshTokenRepository.Where(x => x.UserId == userId).FirstOrDefault();
            if (refreshToken != null)
            {
                return refreshToken.Code;
            }
            var numberByte = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
    }
}