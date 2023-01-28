namespace Project.Infrastructure.SpModels
{
    public class SP_CalcMonthlyPayments
    {
        public int Period { get; set; }
        public DateTime PayDate { get; set; }
        public decimal Payment { get; set; }
        public decimal Current_Balance { get; set; }
        public decimal Interest { get; set; }
        public decimal Principal { get; set; }
    }
}
