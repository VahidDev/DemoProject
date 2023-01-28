namespace Project.Service.ViewModels
{
    public class InvoiceViewModel
    {
        public string InvoiceNr { get; set; }
        public int Period { get; set; }
        public DateTime PayDate { get; set; }
        public decimal Payment { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal Interest { get; set; }
        public decimal Principal { get; set; }
    }
}
