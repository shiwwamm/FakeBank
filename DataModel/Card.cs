using System;
using System.Collections.Generic;

namespace DataModel;

public partial class Card
{
    public long CardId { get; set; }

    public long UserId { get; set; }

    public long AccountId { get; set; }

    public string Number { get; set; } = null!;

    public string Expiration { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
