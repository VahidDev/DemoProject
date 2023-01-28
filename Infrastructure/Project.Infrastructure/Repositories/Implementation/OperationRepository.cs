using Project.Core.Entities.Models;
using Project.Infrastructure.DAL;
using Project.Infrastructure.Repositories.Abstraction;

namespace Project.Infrastructure.Repositories.Implementation
{
    public class OperationRepository
        : GenericRepository<Operation>
        , IOperationRepository
    {
        public OperationRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
