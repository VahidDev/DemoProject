using Project.Core.Utilities.Results;
using Project.Infrastructure.Repositories.Abstraction;
using Project.Service.Services.Abstraction;

namespace Project.Service.Services.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Result GetClientPhoneById(int id)
        {
            var result = new Result();

            var client = _clientRepository.Get(r => r.Id == id && !r.IsDeleted);

            if (client == null)
            {
                result.Success = false;
                result.Error = "Client not found!";
                return result;
            }

            result.Data = client.Phone;

            return result;
        }
    }
}
