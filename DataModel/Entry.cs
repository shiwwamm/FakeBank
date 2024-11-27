using System;
using System.Collections.Generic;

namespace DataModel;

public partial class Entry
{
    public long EntryId { get; set; }

    public long AccountId { get; set; }

    public long Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;
}
