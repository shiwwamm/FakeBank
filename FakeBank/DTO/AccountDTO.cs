namespace FakeBank.DTO
{
    public class AccountDTO
    {
        public long UserId { get; set; }
        public long AccountId { get; set; }
        public long AccountNumber { get; set; }
        public long RoutingNumber { get; set; }
        public string Status { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Balance { get; set; }
        public required string UserEmail { get; set; }

    }
}
