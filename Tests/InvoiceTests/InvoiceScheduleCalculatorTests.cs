using Microsoft.Data.SqlClient;
using Moq;
using Project.Infrastructure.Repositories.Abstraction;
using Project.Infrastructure.SpModels;
using Project.Service.Services.Implementation;
using Project.Service.ViewModels;

namespace InvoiceTests
{
    public class InvoiceScheduleCalculatorTests
    {
        [Fact]
        public void CalculateInvoices_ReturnsExpectedViewModel_ForValidInput()
        {
            //Arrange
            var invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var loanRepositoryMock = new Mock<ILoanRepository>();
            var clientRepositoryMock = new Mock<IClientRepository>();

            var loanService = new LoanService(
                invoiceRepositoryMock.Object,
                orderRepositoryMock.Object,
                loanRepositoryMock.Object,
                clientRepositoryMock.Object
            );

            var model = new CalculateInvoicesViewModel
            {
                LoanAmount = 1000,
                InterestRate = 10,
                LoanPeriod = 12,
                LoanPayOutDate = "2022-01-01"
            };

            //Act
            var result = loanService.CalculateInvoices(model);

            //Assert
            Assert.True(result.Success);
            var invoices = (GetCalculatedInvoicesViewModel)result.Data;
            Assert.NotEmpty(invoices.Invoices);

            var validInvoiceDate = new DateTime(2022,02,01);

            foreach (var invoice in invoices.Invoices)
            {
                Assert.NotNull(invoice.InvoiceNr);
                Assert.True(invoice.Period > 0);
                Assert.True(invoice.PayDate == validInvoiceDate);
                Assert.True(invoice.Payment > 0);
                Assert.True(invoice.CurrentBalance >= 0);
                Assert.True(invoice.Interest > 0);
                Assert.True(invoice.Principal > 0);

                validInvoiceDate = validInvoiceDate.AddMonths(1);
            }
        }

        [Fact]
        public void CalculateInvoices_ShouldReturnValidSequentialInvoiceDatesStartingOneMonthAfterPayout()
        {
            //Arrange
            var loanPayoutDate = new DateTime(2022, 01, 01);
            var model = new CalculateInvoicesViewModel
            {
                LoanAmount = 1000,
                InterestRate = 10,
                LoanPeriod = 12,
                LoanPayOutDate = loanPayoutDate.ToString(),
            };

            var startPaymentDate = loanPayoutDate.AddMonths(1);
            var loanDetailsSpParameters = new List<SqlParameter>
                {
                new SqlParameter(nameof(model.LoanAmount), model.LoanAmount),
                new SqlParameter(nameof(model.InterestRate), model.InterestRate),
                new SqlParameter(nameof(model.LoanPeriod), model.LoanPeriod),
                new SqlParameter(nameof(startPaymentDate), startPaymentDate),
                };

            //Act
            var newInvoices = EfDbTools.ExecuteProcedure<SP_CalcMonthlyPayments>
                ("dbo.Sp_CalcMonthlyPayments", loanDetailsSpParameters);

            //Assert
            Assert.NotEmpty(newInvoices);

            foreach (var invoice in newInvoices)
            {
                startPaymentDate = startPaymentDate.AddMonths(1);
            }
        }
    }
}