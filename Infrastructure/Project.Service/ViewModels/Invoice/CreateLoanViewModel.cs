using System.ComponentModel.DataAnnotations;

namespace Project.Service.ViewModels.Invoice
{
    public class CreateLoanViewModel
    {
        [Required]
        public int Client { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public int Period { get; set; }
        [Required]
        public string PayOutDate { get; set; }
        [Required]
        public ICollection<InvoiceViewModel> Invoices { get; set; }
        public double CurrentBalance { get; set; }
        public double Interest { get; set; }
        public double Principal { get; set; }
    }
}
