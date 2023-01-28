namespace Project.Infrastructure.SpModels
{
    public class SP_GetInvoice
    {
        public long Order { get; set; }
        public string InvoiceNr { get; set; }
        public string InvoiceDate { get; set; }
        public double Amount { get; set; }
    }
}
