using System;

namespace Project.Core.Entities.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientUniqueId { get; set; }
        public string Phone { get; set; }
        public DateTime TrDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
