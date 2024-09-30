using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Regiconnect.Api.Data;
using Regiconnect.Api.DbModels;
using Regiconnect.Api.Models;

namespace Regiconnect.Api.Services
{
    public class SProcLoginService
    {
        private readonly SProcSignupRepository _signupRepository;
        private readonly SProcLoginRepository _loginRepository;

        public SProcLoginService(SProcSignupRepository signupRepository, SProcLoginRepository loginRepository)
        {
            _signupRepository = signupRepository;
            _loginRepository = loginRepository;
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel request)
        {
            var user = await _signupRepository.GetByUsernameAsync(request.Username);

            if (user == null || user.PasswordHash != ComputeSha256Hash(request.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var login = new SProcLogin
            {
                UserId = user.Id,
                LoginTime = DateTime.UtcNow
            };

            await _loginRepository.AddAsync(login);

            return new LoginResponseModel
            {
                Id = login.Id,
                UserId = login.UserId,
                LoginTime = login.LoginTime
            };
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
