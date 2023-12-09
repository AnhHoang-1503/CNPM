namespace CNPM.Domain
{
    public class TemporaryPaper : IHasKey
    {
        public required int Id { get; set; }

        public required TemporaryPaperType Type { get; set; }

        public required int Person_id { get; set; }

        public DateTime? Date_of_issue { get; set; }

        public DateTime? Date_of_expiration { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
