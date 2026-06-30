using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using practice1.Models;
using System.Data;
using Dapper;

namespace practice1.Repository
{
    public class GrievanceRepository : IGrievanceRepository
    {
        private readonly IConfiguration _config;
        public GrievanceRepository(IConfiguration config)
        {
            _config = config;
        }
        private IDbConnection Connection => new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        public void Add(Grievance grievance)
        {
            using var db = Connection;
            string sql = @"
                INSERT INTO grievancedemo   
                (
                EmployeeName,
                IssueTitle,
                Description,
                Priority,
                Status
                 )
                VALUES
                (
                @EmployeeName,
                @IssueTitle,
                @Description,
                @priority,
                @status
                )";
            db.Execute(sql, grievance);
        }

        //public void AddGrievance(Grievance grievance)
        //{
        //    throw new NotImplementedException();
        //}

        public List<Grievance> GetAllGrievances()
        {
            using (var db = Connection)
            {

                string query = "SELECT * FROM grievancedemo";

                return db.Query<Grievance>(query).ToList();
            }
        }
        public Grievance GetById(int id)
        {
            using var db = Connection;

            string sql = "SELECT * FROM grievancedemo WHERE GrevanceId=@Id";

            return db.QueryFirstOrDefault<Grievance>(sql, new { Id = id });
        }
        public void update(Grievance grievance)
        {
            using var db = Connection;

            string sql = @"UPDATE grievancedemo
                   SET EmployeeName=@EmployeeName,
                       IssueTitle=@IssueTitle,
                       Description=@Description,
                       Priority=@Priority,
                       Status=@Status
                   WHERE GrevanceId=@GrevanceId";

            db.Execute(sql, grievance);
        }

        public void Delete(int id)
        {
            using var db = Connection;
            string sql = "DELETE FROM grievancedemo WHERE GrevanceId=@id";
            db.Execute(sql, new { Id = id });
        }
    }
}