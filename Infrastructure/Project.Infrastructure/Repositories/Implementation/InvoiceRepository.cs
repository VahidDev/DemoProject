using Project.Core.Entities.Models;
using Project.Infrastructure.DAL;
using Project.Infrastructure.Repositories.Abstraction;

namespace Project.Infrastructure.Repositories.Implementation
{
    public class InvoiceRepository 
        : GenericRepository<Invoice>
        , IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
