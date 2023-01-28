using System;

namespace Project.Core.Entities.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public string OrderNr { get; set; }
        public decimal Amount { get; set; }
        public decimal PrincipialAmount { get; set; }
        public decimal CurrentBalance { get; set; }
        public double InterestRate { get; set; }
        public int LoanPeriod { get; set; }
        public DateTime PayoutDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime TrDate { get; set; }
    }
}
