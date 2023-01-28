using System;

namespace Project.Core.Entities.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNr { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string OrderNr { get; set; }
        public decimal Amount { get; set; }
        public DateTime TrDate { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
