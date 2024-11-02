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
    [Table("USERS")]
    public partial class User
    {
        [Key]
        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("USERNAME")]
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; } = null!;
        //public string Email { get; set; } = null!;

        [Column("PHONE_NUMBER")]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;

        [InverseProperty("User")]
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
