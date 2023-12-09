namespace CNPM.Domain
{
    public interface IJwtProvider
    {
        public string Generate(User user);

    }
}
