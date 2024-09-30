using System.Data;
using System.Threading.Tasks;
using Dapper;
using Regiconnect.Api.DbModels;

namespace Regiconnect.Api.Data
{
    public class SProcSignupRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public SProcSignupRepository(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task<SProcSignup> GetByUsernameAsync(string username)
        {
            using (var connection = _databaseHelper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<SProcSignup>(
                    "SELECT * FROM Signups WHERE Username = @Username",
                    new { Username = username }
                );
            }
        }

        public async Task AddAsync(SProcSignup signup)
        {
            using (var connection = _databaseHelper.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "INSERT INTO Signups (Username, PasswordHash, Email, CreatedAt) VALUES (@Username, @PasswordHash, @Email, @CreatedAt)",
                    signup
                );
            }
        }
    }
}
