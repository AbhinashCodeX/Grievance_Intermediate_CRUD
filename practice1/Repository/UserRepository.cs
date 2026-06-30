using Dapper;
using Microsoft.Data.SqlClient;
using practice1.Models;
using System.Data;

namespace practice1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection Connection =>
            new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        public User Login(User user)
        {
            using var db = Connection;
            String sql = @"SELECT * FROM Users
            WHERE Username = @Username
            AND Password = @Password";
            return db.QueryFirstOrDefault<User>(sql, user);
        }
        //Registration Ke liye ye logic to interact with Database
        public bool Register(User user)
        {
            using var db = Connection;

            string sql = @"INSERT INTO Users
                   (
                        Username,
                        Password
                   )
                   VALUES
                   (
                        @Username,
                        @Password
                   )";

            int rows = db.Execute(sql, user);

            return rows > 0;
        }
    }
}
