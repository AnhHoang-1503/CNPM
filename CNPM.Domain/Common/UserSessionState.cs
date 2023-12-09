namespace CNPM.Domain.Common
{
    /// <summary>
    /// Quản lý thông tin user mỗi session
    /// </summary>
    public class UserSessionState
    {
        public string? User_Name { get; set; }

        public int? User_Id { get; set; }

        public Role Role { get; set; }
    }
}
