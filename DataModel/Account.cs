using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("ACCOUNTS")]
    public partial class Account
    {
        [Key]
        [Column("ACCOUNT_ID")]
        public int AccountId { get; set; }

        [Column("ACCOUNT_NUMBER")]
        public int AccountNumber { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Accounts")]
        public virtual User User { get; set; } = null!;


    }
}
