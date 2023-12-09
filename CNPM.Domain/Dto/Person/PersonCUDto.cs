namespace CNPM.Domain.Dto.Fee
{
    public class PersonCUDto
    {
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public DateTime? Date_of_birth { get; set; }
        public string? Place_of_birth { get; set; }
        public string? Place_of_origin { get; set; }
        public string? Peoples { get; set; }
        public string? Job { get; set; }
        public string? Place_of_work { get; set; }
        public int? Identify_id { get; set; }
        public DateTime? Date_of_issue { get; set; }
        public string? Place_of_issue { get; set; }
        public DateTime? Date_of_permanent_registration { get; set; }
        public string? Previous_permanent_residence { get; set; }
        public int? Family_id { get; set; }
        public string? Relationship { get; set; }
        public DateTime? Moving_date { get; set; } = null;
        public string? Moving_to { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
        public Gender Gender { get; set; }

    }
}
