using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using PNet_Lab_2.Models;

namespace PNet_Lab_2.Repositories
{
    public class DapperRepository
    {
        private readonly string _connectionString;

        public DapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:BankDb"];
        }

        public IList<Debitor> GetAllDebitors()
        {
            using var db = new SqlConnection(_connectionString);

            var debitors = db.Query<Debitor>("Select * From Debitors Order By Name").ToList();

            return debitors;
        }

        public IList<Debitor> SearchDebitors(string name)
        {
            using var db = new SqlConnection(_connectionString);

            var debitors = db.Query<Debitor>("Select * From Debitors " +
                                             $"Where [Name] Like '%{name}%' " +
                                             "Order By Name").ToList();

            return debitors;
        }

        public ExecuteResult AddDebitor(Debitor debitor)
        {
            using var db = new SqlConnection(_connectionString);

            const string sqlCommand = "Insert Into [Debitors] ([Name], [PostNumber], [PhoneNumber]) " +
                                      "Values (@Name, @PostNumber, @PhoneNumber)";

            try
            {
                db.Execute(sqlCommand, new { debitor.Name, debitor.PostNumber, debitor.PhoneNumber });

                return ExecuteResult.OkResult();
            }
            catch (Exception ex)
            {
                return ExecuteResult.ErrorResult(ex.Message);
            }
        }

        public ExecuteResult UpdateDebitor(Debitor debitor)
        {
            using var db = new SqlConnection(_connectionString);

            const string sqlCommand = "Update [Debitors] " +
                                      "Set [Name]=@Name, [PostNumber]=@PostNumber, [PhoneNumber]=@PhoneNumber " +
                                      "Where [Id]=@Id";

            try
            {
                db.Execute(sqlCommand, new { debitor.Id, debitor.Name, debitor.PostNumber, debitor.PhoneNumber });

                return ExecuteResult.OkResult();
            }
            catch (Exception ex)
            {
                return ExecuteResult.ErrorResult(ex.Message);
            }
        }

        public ExecuteResult DeleteDebitor(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sqlCommand = "Delete From [Debitors] " +
                                      "Where [Id]=@Id";

            try
            {
                db.Execute(sqlCommand, new { Id = id });

                return ExecuteResult.OkResult();
            }
            catch (Exception ex)
            {
                return ExecuteResult.ErrorResult(ex.Message);
            }
        }
    }
}
