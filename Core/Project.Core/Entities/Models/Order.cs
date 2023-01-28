using System;

namespace Project.Core.Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNr { get; set; }
        public DateTime OrderDate { get; set; }
        public string ClientId { get; set; }
        public decimal Amount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime TrDate { get; set; }
    }
}
