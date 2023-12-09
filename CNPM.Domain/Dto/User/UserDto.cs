namespace CNPM.Domain
{
    public class UserDto
    {
        public required int Id { get; set; }

        public required string UserName { get; set; }

        public required Role Role { get; set; }
    }
}
