using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PNet_Lab_2.Helpers;
using PNet_Lab_2.Models;

namespace PNet_Lab_2.Repositories
{
    public class BankRepository
    {
        private readonly string _connectionString;

        public BankRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:BankDb").Get<string>();
        }

        public IList<Debitor> GetAllDebitors()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("Select * From Debitors " +
                                         "Order By Name", connection);

            var reader = command.ExecuteReader();

            var debitors = ReadDebitors(reader);

            return debitors;
        }

        public IList<Debitor> SearchDebitors(string name)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("Select * From Debitors " +
                                         $"Where [Name] Like '%{name}%' " +
                                         "Order By Name", connection);

            var reader = command.ExecuteReader();

            var debitors = ReadDebitors(reader);

            return debitors;
        }

        public IList<Credit> GetDebitorCredits(int debitorId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("Select * From Credits " +
                                         $"Where DebitorId = {debitorId} " +
                                         "Order By OpenDate", connection);

            var reader = command.ExecuteReader();

            var credits = ReadCredits(reader);

            return credits;
        }

        public Credit GetCredit(int creditId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("Select * From Credits " +
                                         $"Where Id = {creditId}", connection);

            var reader = command.ExecuteReader();

            var credit = ReadCredits(reader).FirstOrDefault();

            return credit;
        }

        public IList<Payment> GetCreditPayments(int creditId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("Select * From Payments " +
                                         $"Where CreditId = {creditId} " +
                                         "Order By PaymentDate", connection);


            var reader = command.ExecuteReader();

            var payments = ReadPayments(reader);

            return payments;
        }

        public ExecuteResult AddDebitor(Debitor debitor)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("Insert Into [Debitors] ([Name], [PostNumber], [PhoneNumber]) " +
                                         $"Values (@Name, @PostNumber, @PhoneNumber)", connection);

            command.Parameters.AddWithValue("Name", debitor.Name);
            command.Parameters.AddWithValue("PostNumber", debitor.PostNumber);
            command.Parameters.AddWithValue("PhoneNumber", debitor.PhoneNumber);

            try
            {
                command.ExecuteNonQuery();

                return ExecuteResult.OkResult();
            }
            catch (Exception ex)
            {
                return ExecuteResult.ErrorResult(ex.Message);
            }
        }

        public ExecuteResult AddCredit(Credit credit)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("Insert Into [Credits] ([DebitorId], [Amount], [Balance], [OpenDate]) " +
                                         "Values (@DebitorId, @Amount, @Balance, @OpenDate)", connection);

            command.Parameters.AddWithValue("DebitorId", credit.DebitorId);
            command.Parameters.AddWithValue("Amount", credit.Amount);
            command.Parameters.AddWithValue("Balance", credit.Amount);
            command.Parameters.AddWithValue("OpenDate", credit.OpenDate);

            try
            {
                command.ExecuteNonQuery();

                return ExecuteResult.OkResult();
            }
            catch (Exception ex)
            {
                return ExecuteResult.ErrorResult(ex.Message);
            }
        }

        public ExecuteResult AddPayment(Payment payment)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var transaction = connection.BeginTransaction();

            var command = new SqlCommand("Insert Into [Payments] ([CreditId], [Amount], [PaymentDate]) " +
                                         "Values (@CreditId, @Amount, @PaymentDate)", connection);

            command.Transaction = transaction;

            command.Parameters.AddWithValue("CreditId", payment.CreditId);
            command.Parameters.AddWithValue("Amount", payment.Amount);
            command.Parameters.AddWithValue("PaymentDate", payment.PaymentDate);

            try
            {
                command.ExecuteNonQuery();

                command.CommandText = "Update [Credits] " +
                                      "Set [Balance] = (Balance - @Amount) " +
                                      "Where Id = @CreditId";

                command.ExecuteNonQuery();

                transaction.Commit();

                return ExecuteResult.OkResult();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return ExecuteResult.ErrorResult(ex.Message);
            }
        }

        private IList<Debitor> ReadDebitors(SqlDataReader reader)
        {
            var debitors = new List<Debitor>();

            while (reader.Read())
            {
                var debitor = new Debitor
                {
                    Id = reader["Id"].GetInt(),
                    Name = reader["Name"].GetString(),
                    PhoneNumber = reader["PhoneNumber"].GetString(),
                    PostNumber = reader["PostNumber"].GetString()
                };

                debitors.Add(debitor);
            }

            return debitors;
        }

        private IList<Credit> ReadCredits(SqlDataReader reader)
        {
            var credits = new List<Credit>();

            while (reader.Read())
            {
                var credit = new Credit
                {
                    Id = reader["Id"].GetInt(),
                    Amount = reader["Amount"].GetDecimal(),
                    Balance = reader["Balance"].GetDecimal(),
                    DebitorId = reader["DebitorId"].GetInt(),
                    OpenDate = reader["OpenDate"].GetDate()
                };

                credits.Add(credit);
            }

            return credits;
        }

        private IList<Payment> ReadPayments(SqlDataReader reader)
        {
            var payments = new List<Payment>();

            while (reader.Read())
            {
                var payment = new Payment
                {
                    Id = reader["Id"].GetInt(),
                    Amount = reader["Amount"].GetDecimal(),
                    CreditId = reader["CreditId"].GetInt(),
                    PaymentDate = reader["PaymentDate"].GetDate()
                };

                payments.Add(payment);
            }

            return payments;
        }
    }
}
