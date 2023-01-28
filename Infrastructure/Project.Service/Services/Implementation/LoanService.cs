﻿using Microsoft.Data.SqlClient;
using Project.Core.Entities.Models;
using Project.Core.Utilities.Results;
using Project.Infrastructure.Repositories.Abstraction;
using Project.Infrastructure.SpModels;
using Project.Service.Services.Abstraction;
using Project.Service.ViewModels;
using Project.Service.ViewModels.Invoice;

namespace Project.Service.Services.Implementation
{
    public class LoanService : ILoanService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILoanRepository _loanRepository;

        public LoanService(
            IInvoiceRepository invoiceRepository, 
            IOrderRepository orderRepository,
            ILoanRepository loanRepository)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _loanRepository = loanRepository;
        }

        public Result CalculateInvoices(CalculateInvoicesViewModel model)
        {
            var result = new Result();
            try
            {
                var startPaymentDate = DateTime.Parse(model.LoanPayOutDate).AddMonths(1);

                var loanDetailsSpParameters = new List<SqlParameter>
                {
                new SqlParameter(nameof(model.LoanAmount), model.LoanAmount),
                new SqlParameter(nameof(model.InterestRate), model.InterestRate),
                new SqlParameter(nameof(model.LoanPeriod), model.LoanPeriod),
                new SqlParameter(nameof(startPaymentDate), startPaymentDate),
                };

                var newInvoices = EfDbTools.ExecuteProcedure<SP_CalcMonthlyPayments>
                    ("dbo.Sp_CalcMonthlyPayments", loanDetailsSpParameters);

                var calculatedInvoicesViewModel = new GetCalculatedInvoicesViewModel();

                var totalInterest = CalculateTotalInterest(model.InterestRate, model.LoanPeriod, model.LoanAmount);
                calculatedInvoicesViewModel.TotalInterest = Math.Round(totalInterest,2);

                var latestInvoice = EfDbTools.ExecuteProcedure<SP_KeyValueResult>
                    ("dbo.SP_GetLastInvoice", null).FirstOrDefault();
                var latestInvoiceNumber = 0;

                if (latestInvoice != null)
                {
                    latestInvoiceNumber = int.Parse(latestInvoice.Value);
                }

                var invoices = new List<InvoiceViewModel>();

                foreach (var invoice in newInvoices)
                {
                    var invoiceModel = SP_GetInvoiceToInvoiceViewModel(invoice);
                    latestInvoiceNumber ++;

                    invoiceModel.InvoiceNr = GenerateNumber(latestInvoiceNumber);

                    invoices.Add(invoiceModel);
                }

                calculatedInvoicesViewModel.Invoices = invoices;

                result.Data = calculatedInvoicesViewModel;
            }
            catch (Exception ex)
            {
                result.Success= false;
                result.Error = ex.Message;
            }

            return result;
        }

        private InvoiceViewModel SP_GetInvoiceToInvoiceViewModel(SP_CalcMonthlyPayments invoice)
        {
            return new InvoiceViewModel()
            {
                CurrentBalance = invoice.Current_Balance,
                Interest = invoice.Interest,
                PayDate = invoice.PayDate,
                Payment = invoice.Payment,
                Period = invoice.Period,
                Principal = invoice.Principal,
            };
        }

        public static string GenerateNumber(int currentNumber)
        {
            return currentNumber.ToString().PadLeft(4, '0');
        }

        public ICollection<SP_GetLoan> GetAllLoansInfo()
        {
            var allLoans = EfDbTools.ExecuteProcedure<SP_GetLoan>
                ("dbo.SP_GetAllLoansInfo", null);

            return allLoans;
        }

        public LoanDetailsViewModel GetLoanDetails(int? id)
        {
            var loanDetailsViewModel = new LoanDetailsViewModel();

            if (!id.HasValue)
            {
                var allClientsCombo = EfDbTools.ExecuteProcedure<SP_KeyValueResult>
                    ("dbo.SP_GetAllClientsCombo", null);

                var start = 3;
                var count = 25 - start;
                var loanMonthsCombo = Enumerable.Range(start, count);

                loanDetailsViewModel.ClientsComboBox = allClientsCombo;
                loanDetailsViewModel.LoanMonthsCombo = loanMonthsCombo;
                return loanDetailsViewModel;
            }

            var loanDetailsSpParameters = new List<SqlParameter>
            {
                new SqlParameter(nameof(id), id.Value),
            };

            var loanDetails = EfDbTools.ExecuteProcedure<SP_GetLoanDetails>
                ("dbo.SP_GetLoanDetailsById", loanDetailsSpParameters)
                .FirstOrDefault();

            var invoicesSpParameters = new List<SqlParameter>
            {
                new SqlParameter(nameof(loanDetails.OrderNr), loanDetails.OrderNr),
            };

            var invoices = EfDbTools.ExecuteProcedure<SP_GetInvoice>
                    ("dbo.SP_GetLoanInvoicesByOrderId", invoicesSpParameters);

            loanDetailsViewModel.Id = loanDetails.Id;
            loanDetailsViewModel.LoanDetails = loanDetails;
            loanDetailsViewModel.Invoices = invoices;

            var totalInterest = CalculateTotalInterest(loanDetailsViewModel.LoanDetails.InterestRate,
                                                       loanDetailsViewModel.LoanDetails.LoanPeriod.Value,
                                                       loanDetailsViewModel.LoanDetails.LoanAmount);

            loanDetailsViewModel.TotalInterest = Math.Round(totalInterest, 2);

            return loanDetailsViewModel;
        }

        private double CalculateTotalInterest(double interestRate, int loanPeriod, decimal loanAmount)
        {
            return (double)((loanAmount * (decimal)interestRate) / 100) * loanPeriod;
        }

        public Result IssueLoan(CreateInvoiceViewModel model)
        {
            var result = new Result();

            try
            {
                var lastOrder = EfDbTools.ExecuteProcedure<SP_KeyValueResult>
                 ("dbo.SP_GetLastOrder", null).FirstOrDefault();

                var lastNumber = 0;

                if (lastOrder != null)
                {
                    lastNumber = int.Parse(lastOrder.Value);
                }

                var newOrder = new Order() {
                    Amount = decimal.Parse(model.Amount), 
                    OrderDate = DateTime.Now, 
                    ClientId = model.Client.ToString(),
                    OrderNr = GenerateNumber(lastNumber),
                    TrDate= DateTime.Now,
                };

                _orderRepository.Add(newOrder);

                var loan = CreateInvoiceViewModelToLoan(model);
                loan.OrderNr = newOrder.OrderNr;

                var invoices = new List<Invoice>();

                foreach (var invoice in model.Invoices)
                {
                    Invoice newInvoice = InvoiceViewModelToInvoice(invoice);
                    newInvoice.OrderNr = newOrder.OrderNr;
                    invoices.Add(newInvoice);
                }

                _invoiceRepository.AddRange(invoices);  

                loan.PrincipialAmount = model.Invoices.Sum(r => r.Principal);
                loan.CurrentBalance = model.Invoices.Sum(r => r.CurrentBalance);

                _loanRepository.Add(loan);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                result.Success = false;
            }

            return result;
        }

        private Invoice InvoiceViewModelToInvoice(InvoiceViewModel invoice)
        {
            return new Invoice()
            {
                Amount = invoice.Payment,
                InvoiceDate = DateTime.Now,
                InvoiceNr = invoice.InvoiceNr,
                TrDate = DateTime.Now,
                IsAccepted = true,
            };
        }

        private Loan CreateInvoiceViewModelToLoan(CreateInvoiceViewModel model)
        {
            return new Loan
            {
                Amount= decimal.Parse(model.Amount),
                CurrentBalance= (decimal)model.CurrentBalance,
                InterestRate = model.Interest,
                LoanPeriod = model.Period,
                PayoutDate = DateTime.Parse(model.PayOutDate),
                PrincipialAmount = (decimal)model.Principal,
                TrDate = DateTime.Now
            };
        }
    }
}
