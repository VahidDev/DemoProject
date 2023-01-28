using Microsoft.AspNetCore.Mvc;
using Project.Service.Services.Abstraction;
using Project.Service.ViewModels.Invoice;

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
        public IActionResult IssueLoan([FromBody] CreateInvoiceViewModel model)
        {
            var result = _loanService.IssueLoan(model);
            return new ObjectResult(result);
        }
    }
}
