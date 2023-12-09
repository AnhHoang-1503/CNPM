namespace CNPM.Domain
{
    public interface IHasKey
    {
        public int Id { get; set; }
        public int GetKey();
    }
}
