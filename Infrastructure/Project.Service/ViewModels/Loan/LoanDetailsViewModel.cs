using Project.Infrastructure.SpModels;

namespace Project.Service.ViewModels
{
    public class LoanDetailsViewModel
    {
        public int? Id { get; set; }
        public ICollection<SP_KeyValueResult> ClientsComboBox { get; set; }
        public ICollection<SP_GetInvoice> Invoices { get; set; }
        public SP_GetLoanDetails LoanDetails { get; set; } = new SP_GetLoanDetails();
        public IEnumerable<int> LoanMonthsCombo { get; set; }
        public double TotalInterest { get; set; }
    }
}
