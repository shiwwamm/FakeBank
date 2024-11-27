using System;
using System.Collections.Generic;

namespace DataModel;

public partial class Account
{
    public long AccountId { get; set; }

    public long AccountNumber { get; set; }

    public long RoutingNumber { get; set; }

    public long UserId { get; set; }

    public string Status { get; set; } = null!;

    public string Type { get; set; } = null!;

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();

    public virtual ICollection<Transfer> TransferFromAccounts { get; set; } = new List<Transfer>();

    public virtual ICollection<Transfer> TransferToAccounts { get; set; } = new List<Transfer>();

    public virtual User User { get; set; } = null!;
}
