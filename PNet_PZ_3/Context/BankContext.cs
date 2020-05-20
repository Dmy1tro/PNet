using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PNet_PZ_3.Models;

namespace PNet_PZ_3.Context
{
    public class BankContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Debitor> Debitors { get; set; }

        public BankContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:BankDb").Get<string>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
