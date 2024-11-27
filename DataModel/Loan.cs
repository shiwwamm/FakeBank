using System;
using System.Collections.Generic;

namespace DataModel;

public partial class Loan
{
    public long LoadId { get; set; }

    public long UserId { get; set; }

    public string IssueDate { get; set; } = null!;

    public int Duration { get; set; }

    public int Interest { get; set; }

    public long Amount { get; set; }

    public string Type { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
