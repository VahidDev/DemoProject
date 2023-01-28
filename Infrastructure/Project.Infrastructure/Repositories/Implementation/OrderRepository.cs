using Project.Core.Entities.Models;
using Project.Infrastructure.DAL;
using Project.Infrastructure.Repositories.Abstraction;

namespace Project.Infrastructure.Repositories.Implementation
{
    public class OrderRepository
        : GenericRepository<Order>
        , IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
