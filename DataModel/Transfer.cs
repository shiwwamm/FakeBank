using System;
using System.Collections.Generic;

namespace DataModel;

public partial class Transfer
{
    public long TransferId { get; set; }

    public long FromAccountId { get; set; }

    public long ToAccountId { get; set; }

    public long Amount { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account FromAccount { get; set; } = null!;

    public virtual Account ToAccount { get; set; } = null!;
}
