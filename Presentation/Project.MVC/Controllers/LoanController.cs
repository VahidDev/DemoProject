using Microsoft.AspNetCore.Mvc;
using Project.Service.Services.Abstraction;
using Project.Service.ViewModels;
using Project.Service.ViewModels.Invoice;

namespace Project.MVC.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allLoansInfo = _loanService.GetAllLoansInfo();
            return View(allLoansInfo);
        }

        [HttpGet]
        public IActionResult CreateOrView(int? id)
        {
            var loanDetailsViewModel = _loanService.GetLoanDetails(id);
            return View(loanDetailsViewModel);
        }

        [HttpPost]
        public IActionResult CalculateInvoices([FromBody] CalculateInvoicesViewModel model)
        {
            var loanDetailsViewModel = _loanService.CalculateInvoices(model);
            return new ObjectResult(loanDetailsViewModel);
        }

        [HttpPost]
        public IActionResult IssueLoan([FromBody] CreateInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _loanService.IssueLoan(model);

            if (!result.Success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
