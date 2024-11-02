using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public partial class BankContext : DbContext
    {
        public BankContext() { }

        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }


    }
}
