namespace CNPM.Domain.Entity
{
    public class FamilyFeeMapping : IHasKey
    {
        public int Id { get; set; }
        public int Family_id { get; set; }
        public int Fee_id { get; set; }

        public Boolean Is_paid { get; set; }

        public DateTime? Date_of_payment { get; set; }

        public decimal? Amount { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
