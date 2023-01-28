namespace Project.Infrastructure.SpModels
{
    public class SP_GetLoan
    {
        public int LoanId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public int LoanAmount { get; set; }
        public string PayoutDate { get; set; }
    }
}
