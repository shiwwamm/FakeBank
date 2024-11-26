namespace FakeBank.Data
{
    public class AllUserAndAccounts
    {

        public string first_name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string phone_number { get; set; } = null!;
        public string account_number { get; set; } = null!;
        public string routing_number { get; set; } = null!;
        public string account_status { get; set; } = null!;
        public string account_type { get; set; } = null!;
        public double account_balance { get; set; }
        public string card_number { get; set; } = null!;
        public string card_expiration { get; set; } = null!;
        public int card_cvv { get; set; }
        public string card_status { get; set; } = null!;
        public DateTime loan_issue_date { get; set; }
        public int loan_duration { get; set; }
        public double loan_interest { get; set; }
        public double loan_amount { get; set; }
        public string loan_type { get; set; } = null!;


    }
}
