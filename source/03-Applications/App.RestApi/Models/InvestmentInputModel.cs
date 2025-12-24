namespace App.RestApi
{
    public class InvestmentInputModel
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
        public long[] CoverageIds { get; set; }
    }
}
