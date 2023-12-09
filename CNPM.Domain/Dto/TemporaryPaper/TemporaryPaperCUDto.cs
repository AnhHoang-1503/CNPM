namespace CNPM.Domain
{
    public class TemporaryPaperCUDto
    {
        public required TemporaryPaperType Type { get; set; }

        public required int Person_id { get; set; }

        public DateTime? Date_of_expiration { get; set; }
    }
}
