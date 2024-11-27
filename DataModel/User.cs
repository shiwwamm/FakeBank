using System;
using System.Collections.Generic;

namespace DataModel;

public partial class User
{
    public long UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Number { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
