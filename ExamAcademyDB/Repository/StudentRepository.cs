using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ExamAcademyDB.Repository
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // CREATE
        public void Add(Students student)
        {
            using var connection = GetConnection();
            string sql = "INSERT INTO Students (Name, Rating, Surname) VALUES (@Name, @Rating, @Surname)";
            connection.Execute(sql, student);
        }

        // READ ALL
        public IEnumerable<Students> GetAll()
        {
            using var connection = GetConnection();
            return connection.Query<Students>("SELECT * FROM Students");
        }

        // READ BY ID
        public Students? GetById(int id)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<Students>(
                "SELECT * FROM Students WHERE Id = @Id", new { Id = id });
        }

        // UPDATE
        public void Update(Students student)
        {
            using var connection = GetConnection();
            string sql = "UPDATE Students SET Name = @Name, Surname = @Surname, Rating = @Rating WHERE Id = @Id";
            connection.Execute(sql, student);
        }

        // DELETE
        public void Delete(int id)
        {
            using var connection = GetConnection();
            connection.Execute("DELETE FROM Students WHERE Id = @Id", new { Id = id });
        }
    }
}
