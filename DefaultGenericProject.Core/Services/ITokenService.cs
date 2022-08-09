using DefaultGenericProject.Core.Configuration;
using DefaultGenericProject.Core.DTOs.Tokens;
using DefaultGenericProject.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Token oluşturma işlemi
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        TokenDTO CreateToken(User user);
        /// <summary>
        /// Clientlar için token oluşturma
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        ClientTokenDTO CreateTokenByClient(Client client);
    }
}
