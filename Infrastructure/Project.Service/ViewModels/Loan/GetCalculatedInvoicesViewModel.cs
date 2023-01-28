using Project.Infrastructure.SpModels;


namespace Project.Service.ViewModels
{
    public class GetCalculatedInvoicesViewModel
    {
        public IList<InvoiceViewModel> Invoices { get; set; }
        public double TotalInterest { get; set; }
    }
}
