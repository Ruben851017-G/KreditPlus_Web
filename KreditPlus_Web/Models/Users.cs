namespace KreditPlus_Web.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserFullName { get; set; }
        public string? Password { get; set; }
        public short? UserType { get; set; }
        public string? UserTypeName { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
