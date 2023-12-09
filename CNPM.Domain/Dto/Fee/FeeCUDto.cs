namespace CNPM.Domain
{
    public class FeeCUDto
    {
        public string? Name { get; set; }

        public decimal? Amount { get; set; }

        public FeeType Type { get; set; }
    }
}
