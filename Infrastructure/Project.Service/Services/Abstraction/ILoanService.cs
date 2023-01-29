using Project.Core.Utilities.Results;
using Project.Infrastructure.SpModels;
using Project.Service.ViewModels;

namespace Project.Service.Services.Abstraction
{
    public interface ILoanService
    {
        ICollection<SP_GetLoan> GetAllLoansInfo();
        LoanDetailsViewModel GetLoanDetails(int? id);
        Result IssueLoan(CreateLoanViewModel model);
        Result CalculateInvoices(CalculateInvoicesViewModel model);
        Result IssueLoanFromExternal(CreateLoanViewModel model);
    }
}
