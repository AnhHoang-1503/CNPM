namespace CNPM.Domain
{
    public class FeeByFamilyDto
    {
        public string? Name { get; set; }

        public FeeType Type { get; set; }
        public int Family_id { get; set; }
        public int Fee_id { get; set; }

        public Boolean Is_paid { get; set; }

        public DateTime? Date_of_payment { get; set; }

        public decimal? Amount { get; set; }
    }
}
