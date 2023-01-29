
namespace Project.Service.Utilities.Calculators
{
    public static class InterestCalculator
    {
        public static double CalculateTotalInterest(double interestRate, int loanPeriod, decimal loanAmount)
        {
            return (double)((loanAmount * (decimal)interestRate) / 100) * loanPeriod;
        }
    }
}
