namespace CNPM.Domain
{
    public class Family : IHasKey
    {
        public required int Id { get; set; }
        public int Household_head_id { get; set; }
        public string? House_number { get; set; }
        public string? Street { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
