using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

[Table("accounts")]
[Index("UserId", Name = "IX_accounts_user_id")]
public partial class Account
{
    [Key]
    [Column("account_id")]
    public long AccountId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("account_number")]
    public long AccountNumber { get; set; }

    [Column("routing_number")]
    public long RoutingNumber { get; set; }

    [Column("status")]
    [StringLength(255)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [Column("type")]
    [StringLength(255)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Column("balance", TypeName = "decimal(18, 0)")]
    public decimal Balance { get; set; }

    [Column("created_at")]
    [Precision(3)]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    [InverseProperty("Account")]
    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();

    [InverseProperty("FromAccount")]
    public virtual ICollection<Transfer> TransferFromAccounts { get; set; } = new List<Transfer>();

    [InverseProperty("ToAccount")]
    public virtual ICollection<Transfer> TransferToAccounts { get; set; } = new List<Transfer>();

    [ForeignKey("UserId")]
    [InverseProperty("Accounts")]
    public virtual User User { get; set; } = null!;
}
