using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

[Table("transfers")]
[Index("FromAccountId", Name = "IX_transfers_from_account_id")]
[Index("ToAccountId", Name = "IX_transfers_to_account_id")]
public partial class Transfer
{
    [Key]
    [Column("transfer_id")]
    public long TransferId { get; set; }

    [Column("from_account_id")]
    public long FromAccountId { get; set; }

    [Column("to_account_id")]
    public long ToAccountId { get; set; }

    [Column("amount")]
    public long Amount { get; set; }

    [Column("status")]
    public bool Status { get; set; }

    [Column("created_at")]
    [Precision(3)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("FromAccountId")]
    [InverseProperty("TransferFromAccounts")]
    public virtual Account FromAccount { get; set; } = null!;

    [ForeignKey("ToAccountId")]
    [InverseProperty("TransferToAccounts")]
    public virtual Account ToAccount { get; set; } = null!;
}
