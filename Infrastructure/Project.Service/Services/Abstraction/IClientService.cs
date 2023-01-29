using Project.Core.Utilities.Results;

namespace Project.Service.Services.Abstraction
{
    public interface IClientService
    {
        Result GetClientPhoneById(int id);
    }
}
