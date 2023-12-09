namespace CNPM.Domain
{
    public class FeeDto
    {
        public required int Id { get; set; }

        public string? Name { get; set; }

        public FeeType Type { get; set; }
    }
}
