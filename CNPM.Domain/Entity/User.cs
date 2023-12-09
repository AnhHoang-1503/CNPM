namespace CNPM.Domain
{
    public class User : IHasKey
    {
        public required int Id { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required Role Role { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
