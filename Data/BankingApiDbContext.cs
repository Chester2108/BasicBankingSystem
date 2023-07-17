using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Data
{
    public class BankingApiDbContext : DbContext
    {
        public BankingApiDbContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<AccountModel> Account { get; set; }
        public DbSet<TransferModel> AccountTransaction { get; set; }
    }
}
