using Project.Core.Entities.Models;
using Project.Infrastructure.DAL;
using Project.Infrastructure.Repositories.Abstraction;

namespace Project.Infrastructure.Repositories.Implementation
{
    public class ClientRepository 
        : GenericRepository<Client>
        , IClientRepository
    {
        public ClientRepository(AppDbContext dbContext) :base(dbContext) { }
    }
}
