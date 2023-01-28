using Microsoft.AspNetCore.Mvc;
using Project.Service.Services.Abstraction;
using Project.Service.ViewModels;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost]
        [ModelStateControl]
        public IActionResult IssueLoan([FromBody] CreateLoanViewModel model)
        {
            var result = _loanService.IssueLoanFromExternal(model);
            return new ObjectResult(result);
        }
    }
}
