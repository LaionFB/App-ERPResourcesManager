using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Notes.Models;

namespace AuthTokens.Data
{
    public class AuthTokenDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public AuthTokenDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<AuthToken>().Wait();
        }

        public Task<List<AuthToken>> GetAuthTokensAsync()
        {
            return _database.Table<AuthToken>().ToListAsync();
        }

        public Task<AuthToken> GetAuthTokenAsync(int id)
        {
            return _database.Table<AuthToken>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveAuthTokenAsync(AuthToken AuthToken)
        {
            if (AuthToken.ID != 0)
            {
                return _database.UpdateAsync(AuthToken);
            }
            else
            {
                return _database.InsertAsync(AuthToken);
            }
        }

        public Task<int> DeleteAuthTokenAsync(AuthToken AuthToken)
        {
            return _database.DeleteAsync(AuthToken);
        }
    }
}