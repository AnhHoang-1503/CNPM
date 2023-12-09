namespace CNPM.Domain
{
    public class Fee : IHasKey
    {
        public required int Id { get; set; }

        public string? Name { get; set; }

        public FeeType Type { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
