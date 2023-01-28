using System.ComponentModel.DataAnnotations;

namespace Project.Service.ViewModels.Invoice
{
    public class CreateInvoiceViewModel
    {
        public int Client { get; set; }
        public string Phone { get; set; }
        public string Amount { get; set; }
        public int Period { get; set; }
        public string PayOutDate { get; set; }
        public ICollection<InvoiceViewModel> Invoices { get; set; }
        public double CurrentBalance { get; set; }
        public double Interest { get; set; }
        public double Principal { get; set; }
    }
}
