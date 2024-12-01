namespace FakeBank.DTO
{
    public class UserBalance
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
