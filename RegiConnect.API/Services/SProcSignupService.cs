using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Regiconnect.Api.Data;
using Regiconnect.Api.DbModels;
using Regiconnect.Api.Models;

namespace Regiconnect.Api.Services
{
    public class SProcSignupService
    {
        private readonly SProcSignupRepository _signupRepository;

        public SProcSignupService(SProcSignupRepository signupRepository)
        {
            _signupRepository = signupRepository;
        }

        public async Task<SignupResponseModel> RegisterAsync(SignupRequestModel request)
        {
            var passwordHash = ComputeSha256Hash(request.Password);
            var signup = new SProcSignup
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            await _signupRepository.AddAsync(signup);

            return new SignupResponseModel
            {
                Id = signup.Id,
                Username = signup.Username,
                Email = signup.Email,
                CreatedAt = signup.CreatedAt
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
