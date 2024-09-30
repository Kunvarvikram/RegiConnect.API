using System.Data;
using System.Threading.Tasks;
using Dapper;
using Regiconnect.Api.DbModels;

namespace Regiconnect.Api.Data
{
    public class SProcLoginRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public SProcLoginRepository(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task AddAsync(SProcLogin login)
        {
            using (var connection = _databaseHelper.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "INSERT INTO Logins (UserId, LoginTime) VALUES (@UserId, @LoginTime)",
                    login
                );
            }
        }
    }
}
