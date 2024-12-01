using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

[Table("entries")]
[Index("AccountId", Name = "IX_entries_account_id")]
public partial class Entry
{
    [Key]
    [Column("entry_id")]
    public long EntryId { get; set; }

    [Column("account_id")]
    public long AccountId { get; set; }

    [Column("amount")]
    public long Amount { get; set; }

    [Column("created_at")]
    [Precision(3)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Entries")]
    public virtual Account Account { get; set; } = null!;
}
