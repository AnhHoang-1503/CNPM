namespace CNPM.Domain
{
    public class FamilySpilitDto
    {
        public required int Id { get; set; }

        public required FamilyCUDto FamilyCUDto { get; set; }

        public required List<int> ListMemberIds { get; set; }
    }
}
