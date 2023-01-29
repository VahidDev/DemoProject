using Microsoft.AspNetCore.Mvc;
using Project.Service.Services.Abstraction;

namespace Project.MVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetClientPhone(int clientId)
        {
            var result = _clientService.GetClientPhoneById(clientId);
            return new ObjectResult(result);
        }
    }
}
