namespace Project.Infrastructure.SpModels
{
    public class SP_GetLoanDetails
    {
        public int Id { get; set; }
        public int LoanAmount { get; set; }
        public int ClientId { get; set; }
        public string ClientPhone { get; set; }
        public string Client { get; set; }
        public int? LoanPeriod { get; set; }
        public string OrderNr { get; set; }
        public double InterestRate { get; set; }
        public string PayoutDate { get; set; }
    }
}
