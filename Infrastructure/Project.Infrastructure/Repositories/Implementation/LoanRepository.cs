using Project.Core.Entities.Models;
using Project.Infrastructure.DAL;
using Project.Infrastructure.Repositories.Abstraction;

namespace Project.Infrastructure.Repositories.Implementation
{
    public class LoanRepository 
        : GenericRepository<Loan>
        , ILoanRepository
    {
        public LoanRepository(AppDbContext dbContext) : base(dbContext) {}
    }
}
