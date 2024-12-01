using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

[Table("cards")]
[Index("AccountId", Name = "IX_cards_account_id")]
[Index("UserId", Name = "IX_cards_user_id")]
public partial class Card
{
    [Key]
    [Column("card_id")]
    public long CardId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("account_id")]
    public long AccountId { get; set; }

    [Column("number")]
    [StringLength(255)]
    [Unicode(false)]
    public string Number { get; set; } = null!;

    [Column("expiration")]
    [StringLength(255)]
    [Unicode(false)]
    public string Expiration { get; set; } = null!;

    [Column("cvv")]
    public int Cvv { get; set; }

    [Column("status")]
    [StringLength(255)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [ForeignKey("AccountId")]
    [InverseProperty("Cards")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Cards")]
    public virtual User User { get; set; } = null!;
}
