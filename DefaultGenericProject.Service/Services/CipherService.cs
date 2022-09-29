using DefaultGenericProject.Core.Services;
using Microsoft.AspNetCore.DataProtection;

namespace DefaultGenericProject.Service.Services
{
    public class CipherService : ICipherService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string privateKey = "CwCIZ654dUfsdKcv3n1xDzx";

        public CipherService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(privateKey);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(privateKey);
            return protector.Unprotect(cipherText);

        }
    }
}
