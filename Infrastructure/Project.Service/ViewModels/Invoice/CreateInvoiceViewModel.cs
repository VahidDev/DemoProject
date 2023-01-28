namespace Project.Service.ViewModels.Invoice
{
    public class CreateInvoiceViewModel
    {
        public int Client { get; set; }
        public string Phone { get; set; }
        public double Amount { get; set; }
        public int Period { get; set; }
        public string PayOutDate { get; set; }
        public ICollection<InvoiceViewModel> Invoices { get; set; }
    }
}
