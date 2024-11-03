using System;
using System.Collections.Generic;

namespace DataModel;

public partial class Account
{
    public int AccountId { get; set; }

    public int AccountNumber { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
